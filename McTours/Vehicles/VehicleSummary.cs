using McTours.VehicleDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.Vehicles
{
    public class VehicleSummary
    {
        public int Id { get; set; }
        public string VehicleMakeName { get; set; }
        public string VehicleModelName { get; set; }
        //public List<VehicleDefinition> VehicleDefinitions { get; set; }
        public short Year { get; set; }
        //public FuelType FuelType { get; set; }
        //public string FuelTypeNames
        //{
        //    get
        //    {
        //        return EnumHelper.FuelTypeNames.Contains(FuelType) ? EnumHelper.FuelTypeNames[FuelType] : string.Empty;
        //    }
        //}
        public string PlateNumber { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string RegistrationDateSummary
        {
            get
            {
                return $"{RegistrationDate.Day}/{RegistrationDate.Month}/{RegistrationDate.Year}";
            }
        }
        //public short RegistrationDateDay { get; set; }
        //public short RegistrationDateMount { get; set; }
        //public short RegistrationDateYear { get; set; }

    }
}
