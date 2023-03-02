using McTours.Business.Validator;
using McTours.DataAccess;
using McTours.Domain;
using McTours.VehicleMakes;
using McTours.VehicleModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.Business.Services
{
    public class VehicleModelService
    {
        private readonly McToursContext _context = new McToursContext();
        private readonly VehicleModelValidator _validator = new VehicleModelValidator();
        public CommandResult Create(VehicleModelDto vehicleModelDto)
        {
            if(vehicleModelDto == null)
                throw new ArgumentNullException(nameof(vehicleModelDto));
            try
            {
                var vehicleModel = MapToEntity(vehicleModelDto);
                var validationResult = _validator.Validate(vehicleModel);
                if (validationResult.HasErrors)
                {
                    return CommandResult.Failure(validationResult.ErrorString);
                }
                _context.Add(vehicleModel);
                _context.SaveChanges();
                return CommandResult.Success("Kayıt işlemi başarılı");
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error("Kayıt hatası !", ex);
            }
        }
        public CommandResult Update(VehicleModelDto vehicleModelDto)
        {
            try
            {
                var vehicleModel = MapToEntity(vehicleModelDto);
                var validationResult = _validator.Validate(vehicleModel);
                if (validationResult.HasErrors)
                {
                    return CommandResult.Failure(validationResult.ErrorString);
                }
                _context.Update(vehicleModel);
                _context.SaveChanges();
                return CommandResult.Success("Güncelleme başarılı");
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error("Hatalı güncelleme !!", ex);
            }
        }
        public CommandResult Delete(VehicleModelDto vehicleModelDto)
        {
            try
            {
                var vehicleModel = MapToEntity(vehicleModelDto);
                _context.Remove(vehicleModel);
                _context.SaveChanges();
                return CommandResult.Success();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error(ex);
            }
        }
        public CommandResult Delete(int id)
        {
            return Delete(new VehicleModelDto() { Id = id });
        }
        public VehicleModelDto? GetById(int id)
        {
            try
            {
                var vehicleModelDto = _context.VehicleModels.Select(MapToDto).FirstOrDefault(vehicle => vehicle.Id == id);
                return vehicleModelDto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return null;
            }
        }
        public IEnumerable<VehicleModelDto> GetAll()
        {
            try
            {
                var vehicleModelDto = _context.VehicleModels.Select(MapToDto).ToList();
                return vehicleModelDto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return new List<VehicleModelDto>();
            }
        }
        public IEnumerable<VehicleModelSummary> GetSummaries() 
        {
            try
            {
                return _context.VehicleModels.Select(vehicleModel => new VehicleModelSummary()
                {
                    Id = vehicleModel.Id,
                    Name = vehicleModel.Name,
                    VehicleMakeId = vehicleModel.VehicleMakeId,
                    VehicleMakeName = vehicleModel.VehicleMake.Name
                }).ToList();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return Enumerable.Empty<VehicleModelSummary>();
            }
        }
        private static VehicleModel MapToEntity(VehicleModelDto vehicleModelDto)
        {
            return new VehicleModel()
            {
                Id = vehicleModelDto.Id,
                Name = vehicleModelDto.Name,
                VehicleMakeId = vehicleModelDto.VehicleMakeId
            };
        }
        private static VehicleModelDto MapToDto(VehicleModel vehicleModel)
        {
            return new VehicleModelDto()
            {
                Id = vehicleModel.Id,
                Name = vehicleModel.Name,
                VehicleMakeId = vehicleModel.VehicleMakeId
            };
        }
    }
}
