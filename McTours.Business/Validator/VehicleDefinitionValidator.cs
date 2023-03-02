using McTours.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.Business.Validator
{
    internal class VehicleDefinitionValidator
    {
        public ValidationResult Validate(VehicleDefinition vehicleDefinition)
        {
            var validationResult = new ValidationResult();
            if (vehicleDefinition.Year <= 0)
            {
                validationResult.AddError("Araç yılı boş geçilemez");
            }
            if (vehicleDefinition.LineCount <= 0)
            {
                validationResult.AddError("Araç koltuk sıra sayısı boş geçilemez");
            }
            if (vehicleDefinition.SeatingType <= 0)
            {
                validationResult.AddError("Araç koltuk tipi boş geçilemez");
            }
            if (vehicleDefinition.VehicleModelId <= 0)
            {
                validationResult.AddError("Model alanı boş geçilemez");
            }
            return validationResult;
        }
    }
}
