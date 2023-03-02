using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.Domain
{
    public class Vehicle
    {
        public int Id { get; set; }
        public int VehicleDefinitionId { get; set; }
        public string Name
        {
            get
            {
                return string.Concat(VehicleDefinition.VehicleModel.VehicleMake.Name,
                    " / ",
                    VehicleDefinition.VehicleModel.Name,
                    " - ",
                    PlateNumber);
            }
        }
        public string PlateNumber { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime RegistrationDate { get; set; }

        //Navigation Property
        public VehicleDefinition VehicleDefinition { get; set; }
        public IEnumerable<BusTrip> BusTrips { get; set; }
    }
}
