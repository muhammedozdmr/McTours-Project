using McTours.DataAccess;
using McTours.Domain;
using McTours.VehicleMakes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.Business.Services
{
    public class VehicleMakeService
    {
        /*
          Create
          Update
          Delete
          GetById
          GetAll
         */
        private McToursContext _context = new McToursContext();
        public CommandResult Create(VehicleMakeDto model)
        {
            //model null dönerse döndürülecek hata; create metodu bir tane olarak yapıldı başka overloadı yok
            //haliyle bu  metod model bekliyor eğer null ise zaten direk olarak gömülü hatayı döndürür bu şekilde
            //aşağıdaki if kontrolüne gerek kalmaz direk boş olduğu gibi hata döndürür
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            try
            {
                VehicleMake entity = MapToEntity(model);
                //Validasyon(Validation) kontrolü - Geçerlilik kontrolü
                //if(vehicleMake == null)
                //{
                //    return CommandResult.Failure("Kayıt boş olamaz");
                //}
                if (string.IsNullOrWhiteSpace(entity.Name))
                {
                    return CommandResult.Failure("Marka adı boş geçilemez");
                }
                _context.Add(entity);
                _context.SaveChanges();
                return CommandResult.Success();
            }
            catch (Exception ex)
            {
                return CommandResult.Error("Kayıt İşlemi Başarısız !!", ex);
            }
        }
        public CommandResult Update(VehicleMakeDto vehicleMakeDto)
        {
            try
            {
                var vehicleMake = MapToEntity(vehicleMakeDto);
                _context.Update(vehicleMake);
                _context.SaveChanges();
                return CommandResult.Success("Güncelleme tamamlandı");
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error("Güncelleme hatası!", ex);
            }
        }
        public CommandResult Delete(VehicleMakeDto vehicleMakeDto)
        {
            try
            {
                var vehicleMake = MapToEntity(vehicleMakeDto);
                if(_context.VehicleModels.Any(vehicleModel => vehicleModel.VehicleMakeId == vehicleMake.Id))
                {
                    return CommandResult.Failure("Bu markaya kayıtlı araç modelleri olduğu için silinemez !!");
                }
                _context.Remove(vehicleMake);
                _context.SaveChanges();
                return CommandResult.Success("Silme işlemi başarılı");
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error("Silme işlemi hatası !!", ex);
                throw;
            }
        }
        public CommandResult Delete(int id)
        {
            return Delete(new VehicleMakeDto() { Id = id });
        }
        public VehicleMakeDto? GetById(int id)
        {
            try
            {
                var vehicleMakeDto = _context.VehicleMakes.Select(MapToDto).FirstOrDefault(vehicle => vehicle.Id == id);
                return vehicleMakeDto;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return null;
            }
        }
        public IEnumerable<VehicleMakeDto> GetAll()
        {
            try
            {
                return _context.VehicleMakes.Select(MapToDto).ToList();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return new List<VehicleMakeDto>();
            }
        }
        private static VehicleMake MapToEntity(VehicleMakeDto vehicleMakeDto)
        {
            VehicleMake entity = null;
            if (vehicleMakeDto != null)
            {
                entity = new VehicleMake()
                {
                    Id = vehicleMakeDto.Id,
                    Name = vehicleMakeDto.Name
                };
            }
            return entity;
        }
        private static VehicleMakeDto MapToDto(VehicleMake vehicleMake)
        {
            VehicleMakeDto dto = null;
            if (vehicleMake != null)
            {
                dto = new VehicleMakeDto()
                {
                    Id = vehicleMake.Id,
                    Name = vehicleMake.Name
                };
            }
            return dto;
        }
    }
}
