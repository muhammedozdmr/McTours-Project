using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.BusTrips
{
    public class BusTripTicketSummary
    {
        public int Id { get; set; }
        public string Name { get; set; } //DepartureName - ArrivalName 
        public DateTime Date { get; set; }
        public string VehicleName { get; set; } //Plaka Marka(Model)
        public SeatingType BusSeatingType { get; set; }
        public int BusSeatingLineCount { get; set; }    
        public decimal TicketPrice { get; set; }
    }
}
