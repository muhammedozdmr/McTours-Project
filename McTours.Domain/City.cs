using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.Domain
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<BusTrip>DepartureBusTrips { get; set; }
        public IEnumerable<BusTrip>ArrivalBusTrips { get; set; }
    }
}
