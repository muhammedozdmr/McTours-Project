using McTours.Business.Validator;
using McTours.DataAccess;
using McTours.Domain;
using McTours.VehicleDefinitions;
using McTours.VehicleMakes;
using McTours.VehicleModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.Business.Services
{
    public class VehicleDefinitionService
    {
        private readonly McToursContext _context = new McToursContext();
        private readonly VehicleDefinitionValidator _validator = new VehicleDefinitionValidator();
        private readonly VehicleMakeService _vehicleMakeService = new VehicleMakeService();
        //private readonly FuelTypeService _fuelTypeService = new FuelTypeService();
        public CommandResult Create(VehicleDefinitionDto vehicleDefinitionDto)
        {
            if (vehicleDefinitionDto == null)
                throw new ArgumentNullException(nameof(vehicleDefinitionDto));
            try
            {
                var vehicleDefinition = MapToEntity(vehicleDefinitionDto);
                var validationResult = _validator.Validate(vehicleDefinition);
                if (validationResult.HasErrors)
                {
                    return CommandResult.Failure(validationResult.ErrorString);
                }
                _context.VehicleDefinitions.Add(vehicleDefinition);
                _context.SaveChanges();
                return CommandResult.Success("Kayıt işlemi başarılı");
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error("Kayıt hatası !!", ex);
            }
        }
        public CommandResult Update(VehicleDefinitionDto vehicleDefinitionDto)
        {
            if (vehicleDefinitionDto == null)
                throw new ArgumentNullException(nameof(vehicleDefinitionDto));
            try
            {
                var vehicleDefinition = MapToEntity(vehicleDefinitionDto);
                var validationResult = _validator.Validate(vehicleDefinition);
                if (validationResult.HasErrors)
                {
                    return CommandResult.Failure(validationResult.ErrorString);
                }
                _context.Update(vehicleDefinition);
                _context.SaveChanges();
                return CommandResult.Success("Güncelleme işlemi başarılı");
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error("Güncelleme hatası !!", ex);
                throw;
            }
        }
        public CommandResult Delete(int id)
        {
            return Delete(new VehicleDefinitionDto { Id = id });
        }

        public CommandResult Delete(VehicleDefinitionDto vehicleDefinitionDto)
        {
            try
            {
                var vehicleDefiniton = MapToEntity(vehicleDefinitionDto);
                _context.Remove(vehicleDefiniton);
                _context.SaveChanges();
                return CommandResult.Success("Silme işlemi başarılı");
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error("Silme hatası !!", ex);
            }
        }
        public VehicleDefinitionDto? GetById(int id)
        {
            try
            {
                //getbyıd i bu şekilde yaptığımda ajax update de markaları getiremiyorum bu yüzden amacım ajaxıda kullanarak marka-model işlemi için join işlemi yapmam gerek
                //var vehicleDefinitionDto = _context.VehicleDefinitions.Select(MapToDto).FirstOrDefault(vehicle => vehicle.Id == id);
                var vehicleDefinitionEntity = _context.VehicleDefinitions.Include(def => def.VehicleModel).SingleOrDefault(def => def.Id==id);
                if(vehicleDefinitionEntity != null)
                {
                    var dto = MapToDto(vehicleDefinitionEntity);
                    return dto;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return null;
            }
        }
        public IEnumerable<VehicleDefinitionDto> GetAll()
        {
            try
            {
                var vehicleDefinitionDto = _context.VehicleDefinitions.Select(MapToDto).ToList();
                return vehicleDefinitionDto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return new List<VehicleDefinitionDto>();
            }
        }
        public IEnumerable<VehicleDefinitionSummary> GetSummaries()
        {
            try
            {
                var vehicleDefinition = _context.VehicleDefinitions.Select(vehicleDefinition => new VehicleDefinitionSummary()
                {
                    Id = vehicleDefinition.Id,
                    VehicleModelName = vehicleDefinition.VehicleModel.Name,
                    VehicleMakeName = vehicleDefinition.VehicleModel.VehicleMake.Name,
                    Year = vehicleDefinition.Year,
                    SeatingType = vehicleDefinition.SeatingType,
                    LineCount = vehicleDefinition.LineCount,
                    FuelType = vehicleDefinition.FuelType,
                    Utilities = vehicleDefinition.Utilities
                }).ToList();

                //foreach(var vd in vehicleDefinition)
                //{
                //    vd.FuelTypeName = _fuelTypeService.GetByValue((int)vd.FuelType).Name;
                //}

                return vehicleDefinition;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return Enumerable.Empty<VehicleDefinitionSummary>();
            }
        }

        //Eklendi
        //public IEnumerable<VehicleMakeDto>GetByVehicleMake()
        //{
        //    var make = _vehicleMakeService.GetAll();
        //    return make;
        //}
        //public IEnumerable<VehicleModelDto>GetByVehicleModel(int makeId)
        //{
        //    var models = _context.VehicleModels.Where(v=>v.VehicleMakeId == makeId).Select(v=>new VehicleModelDto { VehicleMakeId = makeId}).ToList();
        //    return models;
        //}
        private static VehicleDefinition? MapToEntity(VehicleDefinitionDto vehicleDefinitionDto)
        {
            var entity = default(VehicleDefinition);
            if (vehicleDefinitionDto != null)
            {
                entity = new VehicleDefinition()
                {
                    Id = vehicleDefinitionDto.Id,
                    VehicleModelId = vehicleDefinitionDto.VehicleModelId,
                    Year = vehicleDefinitionDto.Year,
                    SeatingType = vehicleDefinitionDto.SeatingType,
                    LineCount = vehicleDefinitionDto.LineCount,
                    FuelType = vehicleDefinitionDto.FuelType,
                };
            }
            foreach (var utility in vehicleDefinitionDto.Utilities)
            {
                entity.Utilities = entity.Utilities | utility;
                //entity.Utilities |= utility;
            }

            return entity;
        }

        //MakeId burada mapliceksin
        private static VehicleDefinitionDto MapToDto(VehicleDefinition vehicleDefinition)
        {
            var dto = default(VehicleDefinitionDto);
            if (vehicleDefinition != null)
            {
                dto = new VehicleDefinitionDto()
                {
                    Id = vehicleDefinition.Id,
                    VehicleModelId = vehicleDefinition.VehicleModelId,
                    VehicleMakeId = vehicleDefinition.VehicleModel != null ? vehicleDefinition.VehicleModel.VehicleMakeId : 0,
                    Year = vehicleDefinition.Year,
                    SeatingType = vehicleDefinition.SeatingType,
                    LineCount = vehicleDefinition.LineCount,
                    FuelType = vehicleDefinition.FuelType,
                };
            }

            var allUtilities = Enum.GetValues<Utility>();
            foreach (var utility in allUtilities)
            {
                if (utility != Utility.None && vehicleDefinition.Utilities.HasFlag(utility))
                {
                    dto.Utilities.Add(utility);
                }
            }
            return dto;
        }
    }
}
