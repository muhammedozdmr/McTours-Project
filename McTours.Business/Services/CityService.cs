using McTours.BusTrips;
using McTours.Cities;
using McTours.DataAccess;
using McTours.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.Business.Services
{
    public class CityService
    {
        private readonly McToursContext _context = new McToursContext();
        public IEnumerable<CityDto> GetAll()
        {
            try
            {
                return _context.Cities.Select(MapToDto).ToList();

            }
            catch (Exception ex)
            {
                return new List<CityDto>();
            }
        }

        private static CityDto MapToDto(City city)
        {
            var dto = default(CityDto);
            if(city != null)
            {
                dto = new CityDto()
                {
                    Id = city.Id,
                    Name = city.Name
                };
            }
            return dto;
        }
    }
}
