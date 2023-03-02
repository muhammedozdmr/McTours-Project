using McTours.Business.Services;
using McTours.DataAccess;
using McTours.Domain;
using McTours.VehicleDefinitions;
using McTours.VehicleModels;
using McTours.WebApp.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.CompilerServices;

namespace McTours.WebApp.Controllers
{
    public class VehicleDefinitionsController : Controller
    {
        private readonly VehicleMakeService _vehicleMakeService = new VehicleMakeService();
        private readonly VehicleModelService _vehicleModelService = new VehicleModelService();
        private readonly VehicleDefinitionService _vehicleDefinitionService = new VehicleDefinitionService();
        private readonly McToursContext _context = new McToursContext();
        public IActionResult Index()
        {
            var vehicleDefinition = _vehicleDefinitionService.GetSummaries();
            return View(vehicleDefinition);
        }

        //Bu şekilde defaul değer atıyorum parametreye yani boş değer gelirse 0 olarak alıyor
        //bir nevi overload ediyor ama hep yapmamak gerek bu sefer hangi metot zorunlu parametre alıyor karışır
        private void LoadExtraModels(int vehicleMakeId = 0)
        {
            ViewBag.FuelTypes = EnumHelper.FuelTypesGetAll();
            ViewBag.Utilities = EnumHelper.UtilitiesTypeGetAll();
            ViewBag.Year = DateHelper.GetAvailableYears();
            ViewBag.SeatingTypes = EnumHelper.SeatingTypesGetAll();

            var allVehicleMakes = _vehicleMakeService.GetAll();
            ViewBag.VehicleMakeSelectList = new SelectList(allVehicleMakes, "Id", "Name");

            var allVehicleModels = _vehicleModelService.GetAll();
            var vehicleModelsOfMake = allVehicleModels.Where(model => model.VehicleMakeId == vehicleMakeId);
            ViewBag.VehicleModelSelectList = new SelectList(vehicleModelsOfMake, "Id", "Name");
        }
        //private void LoadExtraModels()
        //{
        //    ViewBag.VehicleMakes = _vehicleMakeService.GetAll();
        //    ViewBag.VehicleMakesSelectList = new SelectList(_vehicleMakeService.GetAll(), "Id", "Name");
        //    ViewBag.VehicleModelsSelectList = new SelectList(_vehicleModelService.GetAll(), "Id", "Name");
        //    ViewBag.VehicleModels = _vehicleModelService.GetAll();  // new SelectList(_vehicleModelService.GetAll(), "Id", "Name");
        //    ViewBag.Year = DateHelper.GetAvailableYears();
        //    ViewBag.FuelTypes = EnumHelper.FuelTypesGetAll();
        //    ViewBag.SeatingTypes = EnumHelper.SeatingTypesGetAll();
        //    ViewBag.Utilities = EnumHelper.UtilitiesTypeGetAll();
        //}
        public IActionResult Create()
        {
            LoadExtraModels();
            return View();
        }
        [HttpPost]
        public IActionResult Create(VehicleDefinitionDto vehicleDefinition)
        {
            LoadExtraModels();
            var commadResult = _vehicleDefinitionService.Create(vehicleDefinition);
            if (commadResult.IsSuccess)
            {
                //DropdownList kullanacaksanız aşağıdaki ideal
                //ViewBag.FuelTypes = new SelectList(EnumHelper.FuelTypesGetAll(), "Value", "Name");

                TempData[Keys.SuccessMessage] = commadResult.Message;
                return RedirectToAction("Index");
            }
            else
            {
                LoadExtraModels();
                TempData[Keys.ErrorMessage] = commadResult.Message;
                return View(vehicleDefinition);
            }
        }
        public IActionResult Update(int id)
        {
            var vehicleDefinition = _vehicleDefinitionService.GetById(id);
            if (vehicleDefinition != null)
            {
                LoadExtraModels(vehicleDefinition.VehicleMakeId);
                return View(vehicleDefinition);
            }
            else
            {
                TempData["ErrorMessage"] = $"{id} ID'li Kayıt bulunamadı";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Update(VehicleDefinitionDto vehicleDefinition)
        {
            var commandResult = _vehicleDefinitionService.Update(vehicleDefinition);

            if (commandResult.IsSuccess)
            {
                TempData["SuccessMessage"] = commandResult.Message;
                return RedirectToAction("Index");
            }
            else
            {
                LoadExtraModels();
                //ViewBag.VehicleMakes = new SelectList(_vehicleMakeService.GetAll(), "Id", "Name");
                //ViewBag.VehicleModels = new SelectList(_vehicleModelService.GetAll(), "Id", "Name");
                //ViewBag.Year = DateHelper.GetAvailableYears();
                //ViewBag.FuelTypes = EnumHelper.FuelTypesGetAll();
                //ViewBag.SeatingTypes = EnumHelper.SeatingTypesGetAll();
                //ViewBag.Utilities = EnumHelper.UtilitiesTypeGetAll();
                ViewData[Keys.ErrorMessage] = commandResult.Message;
                return View(vehicleDefinition);
            }
        }

        public IActionResult Delete(int id)
        {
            var vehicleDefinition = _vehicleDefinitionService.GetById(id);
            if (vehicleDefinition != null)
            {
                ViewBag.VehicleMakes = _vehicleMakeService.GetAll();
                ViewBag.VehicleModels = _vehicleModelService.GetAll();
            }
            else
            {
                TempData["ErrorMessage"] = $"{id} ID'li kayıt bulunamadı !";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(VehicleDefinitionDto definition)
        {
            var commandResult = _vehicleDefinitionService.Delete(definition);
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

        //Eklendi
        //public ActionResult GetMakes()
        //{
        //    var makes = _vehicleDefinitionService.GetByVehicleMake();
        //    return Json(makes);
        //}
        public ActionResult GetModels(int id)
        {
            var models = _vehicleModelService.GetAll();
            var selectedMaketoModelList = models.Where(v=>v.VehicleMakeId==id).ToList();
            return Json(selectedMaketoModelList);
        }
    }
}
