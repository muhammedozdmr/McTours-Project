using McTours.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.Business.Validator
{
    internal class BusTripValidator
    {
        public ValidationResult Validate(BusTrip busTrip)
        {
            var validationResult = new ValidationResult();
            if (busTrip.HasBreakTime == null)
            {
                validationResult.AddError("Mola durumu boş geçilemez");
            }
            if (busTrip.ArrivalCityId == null)
            {
                validationResult.AddError("Varış şehri boş geçilemez");
            }
            if (busTrip.DepartureCityId == null)
            {
                validationResult.AddError("Kalkış şehri boş geçilemez");
            }
            return validationResult;
        }
    }
}
