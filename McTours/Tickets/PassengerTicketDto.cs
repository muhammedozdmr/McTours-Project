using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.Tickets
{
    public class PassengerTicketDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public Gender Gender { get; set; }
        public int BusTripId { get; set; }
        public int SeatNumber { get; set; }
        public decimal TicketPrice { get; set; }
    }
}
