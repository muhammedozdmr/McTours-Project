using McTours.Business.Validator;
using McTours.DataAccess;
using McTours.Domain;
using McTours.Tickets;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.Business.Services
{
    public class TicketService
    {
        private McToursContext _context = new McToursContext();
        private readonly TicketValidator _validator = new TicketValidator();
        public IEnumerable<TicketDto> GetAll()
        {
            try
            {
                return _context.Tickets.Select(MapToDto).ToList();
            }
            catch (Exception ex)
            {
                return new List<TicketDto>();
            }
        }
        public TicketDto GetById(int id)
        {
            try
            {
                var entity = _context.Tickets.Find(id);

                return MapToDto(entity);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());

                return null;
            }
        }
        public CommandResult Create(TicketDto ticketDto)
        {
            if (ticketDto == null)
                throw new ArgumentNullException(nameof(ticketDto));
            try
            {
                var entity = MapToEntity(ticketDto);
                var validationResult = _validator.Validate(entity);
                if (validationResult.HasErrors)
                {
                    return CommandResult.Failure(validationResult.ErrorString);
                }
                _context.Tickets.Add(entity);
                _context.SaveChanges();
                return CommandResult.Success();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error(ex);
            }
        }
        public CommandResult Update(TicketDto model)
        {
            try
            {
                var entity = MapToEntity(model);
                _context.Tickets.Update(entity);
                _context.SaveChanges();
                return CommandResult.Success();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error(ex);
            }
        }
        public CommandResult Delete(TicketDto ticketDto)
        {
            var entity = MapToEntity(ticketDto);
            try
            {
                _context.Tickets.Remove(entity);
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
            return Delete(new TicketDto() { Id = id });
        }
        private static TicketDto MapToDto(Ticket ticket)
        {
            var dto = default(TicketDto);
            if(ticket != null)
            {
                dto = new TicketDto()
                {
                    Id = ticket.Id,
                    BusTripId = ticket.BusTripId,
                    PassengerId = ticket.PassengerId,
                    Price = ticket.Price,
                    SeatNumber = ticket.SeatNumber
                };
            }
            return dto;
        }
        private static Ticket? MapToEntity(TicketDto ticketDto)
        {
            var entity = default(Ticket);
            if(ticketDto != null)
            {
                entity = new Ticket()
                {
                    Id = ticketDto.Id,
                    BusTripId = ticketDto.BusTripId,
                    PassengerId = ticketDto.PassengerId,
                    Price = ticketDto.Price,
                    SeatNumber = ticketDto.SeatNumber
                };
            }
            return entity;
        }
    }
}
