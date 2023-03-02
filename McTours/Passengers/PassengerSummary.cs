using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.Passengers
{
    public class PassengerSummary
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public Gender Gender { get; set; }
        public string BusTripRoute { get; set; }
        public int TicketsCount { get; set; }
        public DateTime LastTicketDate { get; set; }
    }
}
