using Microsoft.AspNetCore.Mvc;

namespace McTours.WebApp.Controllers
{
    public class AjaxController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //Ajax/GetPlayerName
        public string GetPlayerName()
        {
            return "Tsubasa Ozora";
        }

        public IActionResult GetPlayerNames()
        {
            var players = new List<object>()
            {
                new {FirstName = "Tsubasa", LastName="Ozora"},
                new {FirstName = "Taro", LastName="Misaki"},
                new {FirstName = "Kojiro", LastName="Hyuga"},
                new {FirstName = "Jun", LastName="Misugi"},
                new {FirstName = "Ken", LastName="Wakashimazu"}
            };

            //JSON => JavaScript Object Notation
            //Nesnelerin JavaScript nesnesi tipinde/görünümünde string bir biçime çevrilmesi anlamına geliyor.
            return Json(players);
        }
    }
}
