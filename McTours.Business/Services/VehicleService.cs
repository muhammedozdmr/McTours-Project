using McTours.Business.Validator;
using McTours.DataAccess;
using McTours.Domain;
using McTours.Vehicles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace McTours.Business.Services
{
    public class VehicleService
    {
        private readonly McToursContext _context = new McToursContext();
        private readonly VehicleValidator _validator = new VehicleValidator();
        public CommandResult Create(VehicleDto vehicleDto)
        {
            if (vehicleDto == null) throw new ArgumentNullException(nameof(vehicleDto));
            
            try
            {
                var vehicle = MaptoEntity(vehicleDto);
                var validationResult = _validator.Validate(vehicle);
                var charPlate = vehicleDto.PlateNumber;
               
                if (validationResult.HasErrors)
                {
                    return CommandResult.Failure(validationResult.ErrorString);
                }
                for (int i = 0; i < charPlate.Length; i++)
                {
                    if (char.IsLower(charPlate[i]))
                        return CommandResult.Failure("Plaka küçük harf ile girilemez !");
                }
                _context.Add(vehicle);
                _context.SaveChanges();
                return CommandResult.Success("Kayıt işlemi başarılı");
            }
            catch (Exception ex)
            {
                Trace.TraceError($"{DateTime.Now} - {ex}");
                return CommandResult.Error("Kayıt hatası !!", ex);
            }
        }
        public CommandResult Update(VehicleDto vehicleDto)
        {
            try
            {
                var vehicle = MaptoEntity(vehicleDto);
                var validationResult = _validator.Validate(vehicle);
                if (validationResult.HasErrors)
                {
                    return CommandResult.Failure(validationResult.ErrorString);
                }
                _context.Add(vehicle);
                _context.SaveChanges();
                return CommandResult.Success("Güncelleme başarılı");
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error("Güncelleme hatası !!", ex);
            }
        }
        public CommandResult Delete(int id)
        {
            return Delete(new VehicleDto() { Id= id });
        }
        public CommandResult Delete(VehicleDto vehicleDto)
        {
            try
            {
                var vehicle = MaptoEntity(vehicleDto);
                _context.Remove(vehicle);
                _context.SaveChanges();
                return CommandResult.Success("Silme işlemi başarılı");
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error("Silme hatası !!", ex);
            }
        }
        public VehicleDto? GetById(int id)
        {
            try
            {
                var vehicleDto = _context.Vehicles.Select(MaptoDto).FirstOrDefault(vehicle => vehicle.Id == id);
                return vehicleDto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return null;
            }
        }
        public IEnumerable<VehicleDto> GetAll()
        {
            try
            {
                var vehicleDto = _context.Vehicles.Select(MaptoDto).ToList();
                return vehicleDto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return new List<VehicleDto>();
            }
        }
        public IEnumerable<VehicleSummary> GetSummaries()
        {
            try
            {
                return _context.Vehicles.Select(vehicle => new VehicleSummary()
                {
                    Id = vehicle.Id,
                    PlateNumber = vehicle.PlateNumber,
                    RegistrationDate = vehicle.RegistrationDate,
                    RegistrationNumber = vehicle.RegistrationNumber,
                    VehicleMakeName = vehicle.VehicleDefinition.VehicleModel.VehicleMake.Name,
                    VehicleModelName = vehicle.VehicleDefinition.VehicleModel.Name,
                    Year = vehicle.VehicleDefinition.Year,
                    //FuelTypesName = vehicle.VehicleDefinition.FuelType,

                }).ToList();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return Enumerable.Empty<VehicleSummary>();
            }
        }
        public IEnumerable<VehicleHeader> GetVehicleHeader()
        {
            try
            {
                var vehicleHeader = _context.Vehicles.Select(vehicle => new VehicleHeader()
                {
                    Id = vehicle.Id,
                    VehicleMake = vehicle.VehicleDefinition.VehicleModel.VehicleMake.Name,
                    VehicleModel = vehicle.VehicleDefinition.VehicleModel.Name,
                    PlateNumber = vehicle.PlateNumber   
                }).ToList();
                return vehicleHeader;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return Enumerable.Empty<VehicleHeader>();
            }
        }
        public IEnumerable<VehicleDefinitionsHeader> GetByNameDefinition()
        {
            try
            {
                //TODO: Buranın açıklaması lokalde _context Enumdan dolayı sqle çeviremiyor bu yüzden en başta contexti başlattım make ve modeli doldurdum
                //sonrasında Enum sorun çıkartmadı çünkü bağlantı önceden yapıldı dolduruldu!
                var allDefinitions = _context.VehicleDefinitions
                    .Include("VehicleModel.VehicleMake")
                    .ToList();

                return allDefinitions.Select(x => new VehicleDefinitionsHeader()
                {
                    Id = x.Id,
                    Definitions=string.Concat(x.VehicleModel.VehicleMake.Name," - ",x.VehicleModel.Name," - ",x.Year, " - ", EnumHelper.FuelTypeNames[x.FuelType], " - ",x.LineCount)
                }).ToList();
            }
            catch (Exception ex)
            {
                Trace.TraceError($"{DateTime.Now} - {ex}");
                return Enumerable.Empty<VehicleDefinitionsHeader>();
            }
        }
        //public IEnumerable<Vehi>
        public static Vehicle MaptoEntity(VehicleDto vehicleDto)
        {
            return new Vehicle()
            {
                Id= vehicleDto.Id,
                PlateNumber= vehicleDto.PlateNumber,
                RegistrationDate= vehicleDto.RegistrationDate,
                RegistrationNumber= vehicleDto.RegistrationNumber,
                VehicleDefinitionId= vehicleDto.VehicleDefinitionId,
            };
        }
        public static VehicleDto MaptoDto(Vehicle vehicle)
        {
            return new VehicleDto()
            {
                Id= vehicle.Id,
                PlateNumber= vehicle.PlateNumber,
                RegistrationDate= vehicle.RegistrationDate,
                RegistrationNumber= vehicle.RegistrationNumber,
                VehicleDefinitionId= vehicle.VehicleDefinitionId,
            };
        }
    }
}
