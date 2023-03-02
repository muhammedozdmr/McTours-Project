using McTours.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.Business.Validator
{
    internal class VehicleModelValidator
    {
        public ValidationResult Validate(VehicleModel vehicleModel)
        {
            var validationResult = new ValidationResult();
            if (string.IsNullOrWhiteSpace(vehicleModel.Name))
            {
                validationResult.AddError("Araç model adı boş geçilemez");
            }
            if(vehicleModel.VehicleMakeId <= 0)
            {
                validationResult.AddError("Marka alanı boş geçilemez");
            }
            return validationResult;
        }
    }
}
