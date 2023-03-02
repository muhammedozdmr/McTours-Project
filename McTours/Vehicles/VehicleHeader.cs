using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.Vehicles
{
    public class VehicleHeader
    {
        public int Id { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string PlateNumber { get; set; }
        public string Name => string.Concat(VehicleMake, "/", VehicleModel, " - ", PlateNumber);
    }
}
