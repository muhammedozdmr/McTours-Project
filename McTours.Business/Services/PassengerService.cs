using McTours.Business.Validator;
using McTours.BusTrips;
using McTours.DataAccess;
using McTours.Domain;
using McTours.Passengers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.Business.Services
{
    public class PassengerService
    {

        private readonly McToursContext _context = new McToursContext();
        private readonly BusTripService _busTripService = new BusTripService();
        private readonly TicketService _ticketService = new TicketService();
        private readonly PassengerValidator _validator = new PassengerValidator();
        public CommandResult Create(PassengerDto passengerDto)
        {
            if (passengerDto == null)
                throw new ArgumentNullException(nameof(passengerDto));
            try
            {
                var passenger = MapToEntity(passengerDto);
                var validationResult = _validator.Validate(passenger);
                if (validationResult.HasErrors)
                {
                    return CommandResult.Failure(validationResult.ErrorString);
                }
                _context.Passengers.Add(passenger);
                _context.SaveChanges();
                return CommandResult.Success();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error(ex);
            }
        }
        public CommandResult Update(PassengerDto model)
        {
            try
            {
                var entity = MapToEntity(model);

                _context.Passengers.Update(entity);
                _context.SaveChanges();

                return CommandResult.Success();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());

                return CommandResult.Error(ex);
            }
        }
        public CommandResult Delete(PassengerDto passengerDto)
        {
            var entity = MapToEntity(passengerDto);
            try
            {
                _context.Passengers.Remove(entity);
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
            return Delete(new PassengerDto() { Id = id });
        }
        public IEnumerable<PassengerDto> GetAll()
        {
            try
            {
                return _context.Passengers.Select(MapToDto).ToList();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return new List<PassengerDto>();
            }
        }

        public IEnumerable<SearchPassengerResult>SearchPassengers(SearchPassengerModel searchPassengerModel)
        {
            try
            {
                var query = _context.Passengers.AsQueryable(); //AsQueryable IEnumarable döndürüyor ama execute etmiyor sorgulama yapıyor. Araştır.
                if (!string.IsNullOrWhiteSpace(searchPassengerModel.IdentityNumber))
                {
                    query = query.Where(pass => pass.IdentityNumber.StartsWith(searchPassengerModel.IdentityNumber));
                }
                if (!string.IsNullOrWhiteSpace(searchPassengerModel.FirstName))
                {
                    query = query.Where(pass => pass.FirstName.Contains(searchPassengerModel.FirstName));
                }
                if (!string.IsNullOrWhiteSpace(searchPassengerModel.LastName))
                {
                    query = query.Where(pass => pass.LastName.Contains(searchPassengerModel.LastName));
                }

                return query.Select(pass => new SearchPassengerResult()
                {
                    Id = pass.Id,
                    IdentityNumber = pass.IdentityNumber,
                    FirstName = pass.FirstName,
                    LastName = pass.LastName,
                    Gender = pass.Gender
                }).Take(25) //bu demek tabloyu 25 adette sıınrla hepsini sıralama bunu da genelde sayfalamada kullanırız
                  .ToList();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return null;
            }
        }
        public IEnumerable<PassengerSummary> GetSummaries()
        {
            try
            {
                var passenger = _context.Passengers.Include(pass => pass.Tickets)
                    .Select(pass => new PassengerSummary()
                {
                    Id = pass.Id,
                    FullName = pass.FullName,
                    Gender = pass.Gender,
                    TicketsCount = pass.Tickets.Count(),
                    LastTicketDate = pass.LastTicket != null ? pass.LastTicket.Date : default,
                    BusTripRoute = pass.LastTicket != null ? string.Concat(pass.LastTicket.BusTrip.ArrivalCity.Name, " -> ", pass.LastTicket.BusTrip.DepartureCity.Name) : default
                }).ToList();
                
                return passenger;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return new List<PassengerSummary>();
            }
        }
        public PassengerDto GetById(int id)
        {
            try
            {
                var entity = _context.Passengers.Find(id);

                return MapToDto(entity);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());

                return null;
            }
        }


        private static PassengerDto? MapToDto(Passenger passenger)
        {
            var dto = default(PassengerDto);
            if (passenger != null)
            {
                dto = new PassengerDto()
                {
                    Id = passenger.Id,
                    FirstName = passenger.FirstName,
                    LastName = passenger.LastName,
                    DateOfBirth = passenger.DateOfBirth,
                    IdentityNumber = passenger.IdentityNumber,
                    Gender = passenger.Gender
                };
            }
            return dto;
        }

        private static Passenger? MapToEntity(PassengerDto passengerDto)
        {
            var entity = default(Passenger);
            if (passengerDto != null)
            {
                entity = new Passenger()
                {
                    Id = passengerDto.Id,
                    FirstName = passengerDto.FirstName,
                    LastName = passengerDto.LastName,
                    IdentityNumber = passengerDto.IdentityNumber,
                    Gender = passengerDto.Gender,
                    DateOfBirth = passengerDto.DateOfBirth
                };
            }
            return entity;
        }
    }
}
