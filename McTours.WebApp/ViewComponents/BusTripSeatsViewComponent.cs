using McTours.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace McTours.WebApp.ViewComponents
{
    public class BusTripSeatsViewComponent : ViewComponent
    {
        private readonly BusTripService _busTripService = new BusTripService();
        public IViewComponentResult Invoke(int id) //invoke çalıştır yürüt bu componentda 1 tane metod var
            //yine bu class içerisinde private metod yapabilirim hesap kitap için fakat component mantığı tek birşey çalıştırdığı için
            //dışarıya açık bir tane metod var oda invoke
        {
            var busTripSeats = _busTripService.GetBusTripSeats(id);
            return View(busTripSeats);
        }
    }
}
