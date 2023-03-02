using McTours.Business.Services;
using McTours.DataAccess;
using McTours.Passengers;
using McTours.WebApp.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace McTours.WebApp.Controllers
{
    public class PassengersController : Controller
    {
        public readonly McToursContext _context = new McToursContext();
        public readonly PassengerService _passengerService = new PassengerService();
        public readonly BusTripService _busTripService = new BusTripService();



        public IActionResult Index()
        {
            var passenger = _passengerService.GetSummaries();
            return View(passenger);
        }
        public IActionResult SearchPassengers()
        {

            //Return view demek view ı döndür varsa layout unu da ekle döndür demek
            //return View();
            return PartialView();
            //PartialView layout kabul etmez
        }
        //TODO: burayı yap ara butonuna js ile eventhandler ekle
        [HttpPost]
        public IActionResult SearchPassengers(SearchPassengerModel searchPassengersModel)
        {
            var passengers = _passengerService.SearchPassengers(searchPassengersModel);
            return Json(passengers);
        }
        public IActionResult CreatePassenger()
        {
            ViewBag.Genders = EnumHelper.GendersGetAll();
            return PartialView();
        }

        //TODO: Hocaya sor

        [HttpPost]
        public IActionResult CreatePassenger(PassengerDto passengerDto)
        {
            var passenger = _passengerService.Create(passengerDto);
            return Json(passenger);
        }
        public IActionResult Create()
        {
            ViewBag.Passengers = _passengerService.GetAll();
            ViewBag.Genders = EnumHelper.GendersGetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(PassengerDto passenger)
        {
            var result = _passengerService.Create(passenger);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["ErrorMessage"] = result.Message;
                return View(passenger);
            }
        }
        public IActionResult Update(int id)
        {
            var passenger = _passengerService.GetById(id);
            if (passenger != null)
            {
                ViewBag.Passengers = _passengerService.GetAll();
                return View(passenger);
            }
            else
            {
                ViewData["ErrorMessage"] = $"{id} ID'li kayıt Bulunamadı";
                ViewBag.Passengers = _passengerService.GetAll();
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Update(PassengerDto passenger)
        {
            var result = _passengerService.Update(passenger);

            if (result.IsSuccess)
            {
                TempData[Keys.SuccessMessage] = result.Message;
                return RedirectToAction("Index");
            }
            else
            {
                ViewData[Keys.ErrorMessage] = result.Message;
                ViewBag.Passengers = _passengerService.GetAll();
                return View(passenger);
            }
        }
        [HttpPost]
        public IActionResult Delete(PassengerDto passenger)
        {
            var result = _passengerService.Delete(passenger);

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
    }
}
