using McTours.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.Business.Validator
{
    internal class PassengerValidator
    {
        public ValidationResult Validate(Passenger passenger)
        {
            var validationResult = new ValidationResult();
            if (passenger.FirstName == null)
            {
                validationResult.AddError("İsim alanı boş geçilemez");
            }
            if (passenger.LastName == null)
            {
                validationResult.AddError("Soyisim alanı boş geçilemez");
            }
            if (passenger.Gender == null)
            {
                validationResult.AddError("Cinsiyet seçilmelidir !");
            }
            return validationResult;
        }
    }
}
