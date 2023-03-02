﻿ using McTours.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.Business.Validator
{
    internal class VehicleValidator
    {
        public ValidationResult Validate(Vehicle vehicle)
        {
            var validationResult = new ValidationResult();
            if (string.IsNullOrWhiteSpace(vehicle.PlateNumber))
            {
                validationResult.AddError("Plaka bilgisi boş geçilemez");
            }
            else if (vehicle.PlateNumber.Length > 8 || vehicle.PlateNumber.Length < 7)
            {
                validationResult.AddError("Plaka bilgisini doğru giriniz");
            }
            if (string.IsNullOrEmpty(vehicle.RegistrationNumber))
            {
                validationResult.AddError("Ruhsat numarası boş geçilemez");
            }
            else if (vehicle.RegistrationNumber.Length != 8)
            {
                validationResult.AddError("Ruhsat bilgisi hatalı");
            }
            if(vehicle.RegistrationDate == DateTime.Now)
            {
                validationResult.AddError("Trafik çıkış tarihini bilgisi hatası !");
            }
            return validationResult;
        }
    }
}
