using McTours.Business.Services;
using McTours.VehicleModels;
using McTours.WebApp.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace McTours.WebApp.Controllers
{
    public class VehicleModelsController : Controller
    {
        private readonly VehicleMakeService _vehicleMakeService = new VehicleMakeService();
        private readonly VehicleModelService _vehicleModelService = new VehicleModelService();
        public IActionResult Index()
        {
            var vehicleModels = _vehicleModelService.GetSummaries();
            return View(vehicleModels);
        }
        public IActionResult Create()
        {
            ViewBag.VehicleMakes = _vehicleMakeService.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(VehicleModelDto vehicleModel)
        {
            if (vehicleModel.Name == null)
                ViewBag.VehicleMakes = _vehicleMakeService.GetAll();
            var commadResult = _vehicleModelService.Create(vehicleModel);
            if (commadResult.IsSuccess)
            {
                TempData[Keys.SuccessMessage] = commadResult.Message;
                return RedirectToAction("Index");
            }
            else
            {
                //TempData, ViewData gibi magic value'larda oluşabilecek hatalar için Keys diye bir sınıf oluştururum
                //ve bu şekilde string yazım hatalarının önüne geçerim.
                TempData[Keys.ErrorMessage] = commadResult.Message;
                return View(vehicleModel);
            }
        }
        public IActionResult Update(int id)
        {
            var vehicleModel = _vehicleModelService.GetById(id);
            if (vehicleModel != null)
            {
                //ViewBag.VehicleMakes = _vehicleMakeService.GetAll();
                //asp.net ile foreach kullanmadan frontende SelectItem ile SelectListItem gönderiyoruz.

                var vehicleMakes = _vehicleMakeService.GetAll();
                ViewBag.VehicleMakes = new SelectList(vehicleMakes, "Id", "Name");
                return View(vehicleModel);
            }
            else
            {
                TempData["ErrorMessage"] = $"{id} ID'li Kayıt bulunamadı";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Update(VehicleModelDto vehicleModel)
        {
            var commandResult = _vehicleModelService.Update(vehicleModel);
            if (commandResult.IsSuccess)
            {
                TempData["SuccessMessage"] = commandResult.Message;
                return RedirectToAction("Index");
            }
            else
            {
                //ViewBag.VehicleMakes = _vehicleMakeService.GetAll();
                var vehicleMakes = _vehicleMakeService.GetAll();
                ViewBag.VehicleMakes = new SelectList(vehicleMakes, "Id", "Name");
                ViewData[Keys.ErrorMessage] = commandResult.Message;
                return View(vehicleModel);
            }
        }

        public IActionResult Delete(int id)
        {
            var vehicleModel = _vehicleModelService.GetById(id);
            if (vehicleModel != null)
            {
                ViewBag.VehicleMakes = _vehicleMakeService.GetAll();
                //return View(vehicleModel);
            }
            else
            {
                TempData["ErrorMessage"] = $"{id} ID'li kayıt bulunamadı !";
            }
            return RedirectToAction("Index");
        }

        //Silme ekranı yok direkt javascript ile browser messageboxı çıkartıcaz
        [HttpPost]
        public IActionResult Delete(VehicleModelDto model)
        {
            var commandResult = _vehicleModelService.Delete(model);
            if (commandResult.IsSuccess)
            {
                TempData[Keys.SuccessMessage] = commandResult.Message;
            }
            else
            {
                TempData[Keys.ErrorMessage] = commandResult.Message;
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult GetByMakeId(int makeId)
        {
            var vehicleModels = _vehicleModelService.GetAll();
            //var vehicleModelsByMakeId = new List<VehicleModelDto>();
            //foreach (var vehicleModel in vehicleModels)
            //{
            //    if (vehicleModel.VehicleMakeId==id)
            //    {
            //        vehicleModelsByMakeId.Add(vehicleModel);
            //    }
            //}
            var vehicleModelsByMakeId = vehicleModels.Where(model => model.VehicleMakeId== makeId);
            return Json(vehicleModelsByMakeId);
        }
    }
}
