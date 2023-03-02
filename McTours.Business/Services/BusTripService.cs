using McTours.Business.Validator;
using McTours.BusTrips;
using McTours.Cities;
using McTours.DataAccess;
using McTours.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.Business.Services
{
    public class BusTripService
    {
        private readonly McToursContext _context = new McToursContext();
        private readonly VehicleService _vehicleService = new VehicleService();
        private readonly BusTripValidator _validator = new BusTripValidator();

        public CommandResult Create(BusTripDto busTripDto)
        {
            if (busTripDto == null)
                throw new ArgumentNullException(nameof(busTripDto));
            try
            {
                var busTrip = MapToEntity(busTripDto);
                var validataionResult = _validator.Validate(busTrip);
                if (validataionResult.HasErrors)
                {
                    return CommandResult.Failure(validataionResult.ErrorString);
                }
                _context.BusTrips.Add(busTrip);
                _context.SaveChanges();
                return CommandResult.Success();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error(ex);
            }
        }
        public IEnumerable<BusTripDto> GetAll()
        {
            try
            {
                return _context.BusTrips.Select(MapToDto).ToList();

            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return new List<BusTripDto>();
            }
        }
        public BusTripTicketSummary? GetSummaryById(int id)
        {
            try
            {
                return _context.BusTrips.Select(trip => new BusTripTicketSummary
                {
                    Id = trip.Id,
                    Name = trip.Name,
                    Date = trip.Date,
                    TicketPrice = trip.TicketPrice,
                    VehicleName = trip.Vehicle.Name,
                    BusSeatingType = trip.Vehicle.VehicleDefinition.SeatingType,
                    BusSeatingLineCount = trip.Vehicle.VehicleDefinition.TotalSeatCount
                }).FirstOrDefault(trip => trip.Id == id);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return null;
            }
        }
        public IEnumerable<BusTripSummary> GetSummaries()
        {
            try
            {
                var busTrip = _context.BusTrips.Select(busTrip => new BusTripSummary()
                {
                    Id = busTrip.Id,
                    ArrivalCityName = busTrip.ArrivalCity.Name,
                    Date = busTrip.Date,
                    VehicleMake = busTrip.Vehicle.VehicleDefinition.VehicleModel.VehicleMake.Name,
                    VehicleModel = busTrip.Vehicle.VehicleDefinition.VehicleModel.Name,
                    BreakTimeDuration = busTrip.BreakTimeDuration,
                    VehiclePlate = busTrip.Vehicle.PlateNumber,
                    DepartureCityName = busTrip.DepartureCity.Name,
                    EstimatedDuration = busTrip.EstimatedDuration / 60,
                    TicketPrice = busTrip.TicketPrice
                }).ToList();
                return busTrip;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return Enumerable.Empty<BusTripSummary>();
            }
        }
        public BusTripSeatsModel? GetBusTripSeats(int id)
        {
            try
            {
                var busTripSeats = _context.BusTrips.Select(busTrip => new BusTripSeatsModel()
                {
                    BusTripId = busTrip.Id,
                    BusSeatingType = busTrip.Vehicle.VehicleDefinition.SeatingType,
                    BusSeatingLineCount = busTrip.Vehicle.VehicleDefinition.LineCount,
                    //SoldSeatNumbers = busTrip.Tickets.Select(tic => tic.SeatNumber).ToList() //bu sorgu SoldSeatNumbers ın set parametresi kapalı olduğu için iş görmez
                    //foreach yapıcaz o yüzden 
                }).FirstOrDefault(seats => seats.BusTripId == id);
                var soldTickets = _context.Tickets.Where(tic => tic.BusTripId == id).ToList();
                foreach(var soldTicket in soldTickets)
                {
                    busTripSeats.SoldSeatNumbers.Add(soldTicket.SeatNumber);
                }
                return busTripSeats;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return null;
            }
        }
        public BusTripDto? GetById(int id)
        {
            try
            {
                var entity = _context.BusTrips.Find(id);
                return MapToDto(entity);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return null;
            }
        }
        public CommandResult Update(BusTripDto model)
        {
            try
            {
                var entity = MapToEntity(model);

                _context.BusTrips.Update(entity);
                _context.SaveChanges();

                return CommandResult.Success();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());

                return CommandResult.Error(ex);
            }
        }
        public CommandResult Delete(BusTripDto busTripDto)
        {
            var entity = MapToEntity(busTripDto);
            try
            {
                _context.BusTrips.Remove(entity);
                _context.SaveChanges();
                return CommandResult.Success();
            }
            catch (Exception ex)
            {
                return CommandResult.Error(ex);
            }
        }
        public CommandResult Delete(int id)
        {
            return Delete(new BusTripDto() { Id = id });
        }
        private static BusTrip? MapToEntity(BusTripDto busTripDto)
        {
            var entity = default(BusTrip);
            if (busTripDto != null)
            {
                entity = new BusTrip()
                {
                    Id = busTripDto.Id,
                    TicketPrice = busTripDto.TicketPrice,
                    Date = busTripDto.Date,
                    EstimatedDuration = busTripDto.EstimatedDuration * 60,
                    ArrivalCityId = busTripDto.ArrivalCityId,
                    DepartureCityId = busTripDto.DepartureCityId,
                    VehicleId = busTripDto.VehicleId,
                };
            }
            return entity;
        }
        private static BusTripDto MapToDto(BusTrip busTrip)
        {
            var dto = default(BusTripDto);
            if (busTrip != null)
            {
                dto = new BusTripDto()
                {
                    Id = busTrip.Id,
                    TicketPrice = busTrip.TicketPrice,
                    Date = busTrip.Date,
                    EstimatedDuration = busTrip.EstimatedDuration / 60,
                    ArrivalCityId = busTrip.ArrivalCityId,
                    DepartureCityId = busTrip.DepartureCityId,
                    VehicleId = busTrip.VehicleId
                };
            }
            return dto;
        }
    }
}
