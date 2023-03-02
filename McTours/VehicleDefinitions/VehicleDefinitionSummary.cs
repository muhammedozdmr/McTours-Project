using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.VehicleDefinitions
{
    public class VehicleDefinitionSummary
    {
        public int Id { get; set; }
        public int VehicleModelId { get; set; }
        public string VehicleModelName { get; set; }
        public string VehicleMakeName { get; set; }
        public short Year { get; set; }
        public SeatingType SeatingType { get; set; }
        //TODO: Hocanın kodu değiştir
        public string SeatingTypeName
        {
            get
            {
                return EnumHelper.SeatingTypeNames.ContainsKey(SeatingType) ? EnumHelper.SeatingTypeNames[SeatingType] : string.Empty;
            }
        }
        public int LineCount { get; set; }
        public FuelType FuelType { get; set; }
        public string FuelTypeName
        {
            get
            {
                return EnumHelper.FuelTypeNames.ContainsKey(FuelType) ? EnumHelper.FuelTypeNames[FuelType] : string.Empty;
            }
        }
        public Utility Utilities { get; set; }
        public string UtilitiesName => Utilities.GetUtilitiesNames();
        
    }
}
