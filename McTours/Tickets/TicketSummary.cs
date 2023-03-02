using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.Tickets
{
    public class TicketSummary
    {
        public int Id { get; set; }
        public string BusTripDetail { get; set; }
        public string PassengerFullName { get; set; }
        public decimal Price { get; set; }
    }
}
