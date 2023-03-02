using McTours.Cities;
using McTours.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.BusTrips
{
    public class BusTripSummary
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string DisplayDate => string.Concat(Date.ToString("yyyy-MM-dd HH:mm"));
        public int EstimatedDuration { get; set; }
        public decimal TicketPrice { get; set; }
        public string DisplayVehicleName => string.Concat(VehicleMake, " - ", VehicleModel," - ",VehiclePlate);
        public string DisplayTripSummary => string.Concat(DepartureCityName, " - ", ArrivalCityName, " - ", Date.ToString("yyyy-MM-dd HH:mm"));
        public string VehiclePlate { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string DepartureCityName { get; set; }
        public string ArrivalCityName { get; set; }
        public int? BreakTimeDuration { get; set; }
    }
}
