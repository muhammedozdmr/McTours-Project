using McTours.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.Business.Validator
{
    internal class TicketValidator
    {
        public ValidationResult Validate(Ticket ticket)
        {
            var validationResult = new ValidationResult();
            if (ticket.Price == null)
            {
                validationResult.AddError("Fiyat bilgisi boş geçilemez");
            }
            return validationResult;
        }
    }
}
