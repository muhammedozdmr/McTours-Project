using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.BusTrips
{
    public class BusTripSeatsModel
    {
        public BusTripSeatsModel()
        {
            SoldSeatNumbers = new List<int>();
        }
        public int BusTripId { get; set; }
        public SeatingType BusSeatingType { get; set; }
        public int BusSeatingLineCount { get; set; }
        public ICollection<int>SoldSeatNumbers { get;}
    }
}
