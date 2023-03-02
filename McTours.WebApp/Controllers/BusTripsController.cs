using McTours.Business.Services;
using McTours.BusTrips;
using McTours.Cities;
using McTours.DataAccess;
using McTours.WebApp.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace McTours.WebApp.Controllers
{
    public class BusTripsController : Controller
    {
        private readonly BusTripService _busTripService = new BusTripService();
        private readonly CityService _cityService = new CityService();
        private readonly McToursContext _context = new McToursContext();
        private readonly VehicleService _vehicleService = new VehicleService();

        public IActionResult Index()
        {
            var busTrips = _busTripService.GetSummaries();
            return View(busTrips);
        }
        private void LoadExtraModels()
        {
            ViewBag.VehicleSelectedList = new SelectList(_vehicleService.GetVehicleHeader(), "Id", "Name");
            ViewBag.DepartureCitySelectedList = new SelectList(_cityService.GetAll(), "Id", "Name");
            ViewBag.ArrivalCitySelectedList = new SelectList(_cityService.GetAll(), "Id", "Name");
        }

        public IActionResult Create()
        {
            LoadExtraModels();
            return View();
        }
        [HttpPost]
        public IActionResult Create(BusTripDto busTrips)
        {
            LoadExtraModels();
            var result = _busTripService.Create(busTrips);

            if (result.IsSuccess)
            {
                TempData[Keys.SuccessMessage] = result.Message;
                return RedirectToAction("Index");
            }
            else
            {
                LoadExtraModels();
                ViewData["ErrorMessage"] = result.Message;
                return View(busTrips);
            }
        }
        public IActionResult Update(int id)
        {
            var busTrip = _busTripService.GetById(id);
            if (busTrip != null)
            {
                LoadExtraModels();
                return View(busTrip);
            }
            else
            {
                ViewData["ErrorMessage"] = $"{id} ID'li kayıt Bulunamadı";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Update(BusTripDto busTrip)
        {
            var result = _busTripService.Update(busTrip);

            if (result.IsSuccess)
            {
                TempData[Keys.SuccessMessage] = result.Message;
                return RedirectToAction("Index");
            }
            else
            {
                ViewData[Keys.ErrorMessage] = result.Message;
                LoadExtraModels();
                return View(busTrip);
            }
        }

        public IActionResult Delete(int id)
        {
            var busTrip = _busTripService.GetById(id);
            if (busTrip != null)
            {
                LoadExtraModels();
            }
            else
            {
                TempData["ErrorMessage"] = $"{id} ID'li kayıt bulunamadı !";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(BusTripDto busTrip)
        {
            var result = _busTripService.Delete(busTrip);

            if (result.IsSuccess)
            {
                TempData[Keys.SuccessMessage] = result.Message;
            }
            else
            {
                TempData[Keys.ErrorMessage] = result.Message;
            }
            return RedirectToAction("Index");
        }
        //Seyahat ekranından bilet satışı yapılacak haliyle bilet ile ilgili kontrol seyahat controllerda yapılır aynı şekilde bilete ait seyahat bilgileri de seyahat servisinden gelir

        //BusTrips/Tickets/10
        //BusTrips/10/Tickets
        //[Route("[controller]/{id}/Tickets")]        
        [Route("[controller]/{id}/[action]")]
        public IActionResult Tickets(int id)
        {
            var busTripSummary = _busTripService.GetSummaryById(id);
            return View(busTripSummary);
        }
    }
}
