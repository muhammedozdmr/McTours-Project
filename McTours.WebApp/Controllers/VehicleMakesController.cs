using McTours.Business.Services;
using McTours.Domain;
using McTours.VehicleMakes;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace McTours.WebApp.Controllers
{
    public class VehicleMakesController : Controller
    {
        private readonly VehicleMakeService _vehicleMakeService = new VehicleMakeService();

        //Bütün kayıtların döndürüldüğü tablo olarak kullanıyoruz indexi
        public IActionResult Index()
        {
            var vehicleMakes = _vehicleMakeService.GetAll();
            return View(vehicleMakes);
        }

        //VehicleMakes/Create
        public IActionResult Create()
        {
            return View();
        }

        //VehicleMakes/Create
        [HttpPost]
        public IActionResult Create(VehicleMakeDto vehicleMake)
        {
            var commandResult = _vehicleMakeService.Create(vehicleMake);
            if (commandResult.IsSuccess)
            {
                //TempData üzerindeki veriler, en az bir kez okunana kadar (get edilene kadar) sunucuda muhafaza edilir.
                //En az bir kez okunduğunda, Response tamamlandıktan sonra silinir.
                TempData["ResultMessage"] = commandResult.Message;
                return RedirectToAction("Index");
            }
            else
            {
                //Create metodu iki tane olduğu için ve aynı url'e sahip olduğu için biri get biri post olduğu için
                //bundan dolayı olduğu sayfayı view ediyorum viewda create diye bir view olduğu için ve boş create metodu da return
                //olarak view döndürdüğü için aynı işlemi yapmış oluyorum

                //ViewBag, ViewData, Tempdata
                //Bir View'a model dışında ekstra bilgi taşımanın yöntemleri

                //TempData["ResultMessage"] = commandResult.Message;

                //ViewData aynı action içerisinde kalır farklı actiona gittiği anda silinir. Başka actionda görünmez.
                //TempData ise oku yada okuma diğer actionlara gittiğinde okunduğu anda silinir yani
                //anlık olarak alacağın data varsa view kullan böylece o sayfanın içeriğinde actionunda kalacak fakat o sayfadan başka sayfaya 
                //action gidecekse ve yine data kullanman mesaj kullanman gerekirse temp data kullan

                //ViewData ise adından anlaşılacağı üzere doğru View ile ilgilidir. Eğer bir action View() döndürüyorsa ViewDAta üzerindeki
                //değerler View tarafında okunabilir. Eğer action metodu View dışında başka bir result döndürüyorsa
                //(RedirectToAction, Content, Json vs..) ViewData muhafaza edilmez.

                //ViewData -> Dictionary<string, object> tipinde
                //ViewData["ResultMessage"] = commandResult.Message;

                //Viewbag -> dynamic tipinde
                ViewBag.ResultMessage = commandResult.Message;

                //Aslında ViewData ile ViewBag nesneleri aynı nesneler
                //Sadece kullanım (yazım) şekli farklı; setaksları farklı

                //ViewData["Key"] = "Tsubasa" şeklinde st ettiğiniz bir değeri
                //ViewBag üzerinden var name = ViewBag.Key şeklinde okuyabilirsiniz.
                return View();
            }
        }
        //neyi güncellemek istediğimi sisteme söylüyorum id gönderiyorum
        public IActionResult Update(int id)
        {
            var vehicleMake = _vehicleMakeService.GetById(id);
            if (vehicleMake != null)
            {
                return View(vehicleMake);
            }
            else
            {
                TempData["ErrorMessage"] = $"{id} ID'li Kayıt bulunamadı";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Update(VehicleMakeDto vehicleMake)
        {
            var commandResult = _vehicleMakeService.Update(vehicleMake);
            if (commandResult.IsSuccess)
            {
                TempData["SuccessMessage"] = commandResult.Message;
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["ErrorMessage"] = commandResult.Message;
                //Kullanıcının yazdığı veri tabanına göndermek istediği son değeri geri gönderiyorum veri tabanındaki modeli değil.
                return View(vehicleMake);
            }
        }
        public IActionResult Delete(int id)
        {
            var vehicleMake = _vehicleMakeService.GetById(id);
            if (vehicleMake != null)
            {
                return View(vehicleMake);
            }
            else
            {
                TempData["ErrorMessage"] = $"{id} ID'li Kayıt bulunamadı";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Delete(VehicleMakeDto model)
        {
            var commandresult = _vehicleMakeService.Delete(model);
            if (commandresult.IsSuccess)
            {
                //var name = model.Name; hocaya sor bak hocannın koduna
                TempData["SuccessMessage"] = commandresult.Message;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage=commandresult.Message;
                return View(model);
            }
        }
        // /VehicleMakes/Delete
        //[HttpPost("Delete")]
        //bu 
        //public IActionResult DeleteConfirmed(int id)
        //{
        //    var commandresult = _vehicleMakeService.Delete(model);
        //    if (commandresult.IsSuccess)
        //    {
        //        var name = model.Name;
        //        TempData["ResultMessage"] = commandresult.Message;
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        TempData["ResultMessage"] = commandresult.Message;
        //        return RedirectToAction("Index");
        //    }
        //}

        //Yöntem 1
        //    //en ilkel haliyle ekrandan gelen requesti bu şekilde sunucuya gönderiyorum.
        //    //"name" parametresi cshtmlde input için verilen name attribute isminden gelir
        //    //name attribute ismi "muhittin" olsaydı burası da "muhittin" olurdu.

        //public IActionResult CreateConfirmed()
        //{
        //    var makeName = Request.Form["name"];
        //    var vehicleMakeDto = new VehicleMakeDto()
        //    {
        //        Name = makeName,
        //    };
        //    _vehicleMakeService.Create(vehicleMakeDto);
        //    return View();
        //}

        //Yöntem2
        //    //Interface yolarak tipi IFormCollection olan formValues parametresi
        //    //form ekranında girilen bütün input değerlerini submit edildiği anda bünyesinde barındırır.
        //    //Request.Form işlemini yapar fakat bir dizi gibi kaç adet input değeri varsa onu barındırır
        //    //gelen input değerlerini aşağıda key name yani attribute ismini seçerek çekip alırım
        //    //gelen parametredeki değerlerden key namelerden yani attribute isimlerine göre işlem yaparım 5 değer gelir
        //    //1 tane attribute kullanırsam sadece o attribute için işlem yaparım

        //public IActionResult CreateConfirmed(IFormCollection formValues)
        //{
        //    var makeName = formValues["name"];
        //    var vehicleMakeDto = new VehicleMakeDto()
        //    {
        //        Name = makeName,
        //    };
        //    _vehicleMakeService.Create(vehicleMakeDto);
        //    return View();
        //}


        //Yöntem 3
        //form üzerinde ne kadar input var ise o input değerlerinin key nameine göre parametre ismini gönderirim
        //bu durumun yöntem 1 ve 2 den farkı diğerlerinde eğer formdan integer bir değer gelirse parse etmek zorunda kalırım çünkü request
        //stringvalues diye bir değer döndürürü bu yüzden parse etmem lazım fakat yöntem 3 de parametrenin tipini bildiğim için direkt olarak
        //instance alıp propertye gelen parametreyi yani attribute u atıyorum.
        //public IActionResult CreateConfirmed(string name)
        //{
        //    var vehicleMakeDto = new VehicleMakeDto()
        //    {
        //        Name = name,
        //    };
        //    _vehicleMakeService.Create(vehicleMakeDto);
        //    return View();
        //}

        //Yöntem 4
        //Clienttan sunucuya model gönderilmiş gibi düşünülebilir.
        //Doğrudan model olarak karşılamak.
        //Bu şekilde yapıldığında model form post edildiğinde olan attributeları
        //alır formda işlenmemiş attributeları fakat modelde olan propertyleri görmezden gelir
        //böylelikle tek tek parametreleri işlememiş oluruz

        //formda bulunan method için get ve post arasındaki fark şu şekilde;
        //Get: urlde kayıt işlemi esnasında ? ile başlayıp kayıt edilen veriyi query'e gönderir bu da bize güvenliksiz kayıtlar getirir
        //bu şekilde kullanıcı formu açmadan sadece urlde kayıt edilecek objeyi yazması ile veri tabanına veriyi kayıt eder
        //daha kötüsü başka sitelerde girilen kayıtların arka planda benim url'imi verdiklerinde veri tabanıma kayıt eder
        //Post: url üzerinde kayıt edilen veriyi göstermez FAKAT DİKKAT formun methodunun Post olması url üzerinden kayıt edilemeyeceği anlamına gelmez
        //Bu şekilde her ne kadar urlde gizli olsa da formdan gelen veri, kullanıcı kendisi urlde yazarak yine aynı güvenlik açığına sebep olabilir bu yüzden hem
        //form da post olması lazım hemde kayıt-delete-update crud işlemlerinin yapıldığı methodlar aşağıdaki gibi
        //HttpPost olarak etiketlenmesi gerek bunun anlamı bu metod sadece post esnasında kullanılabilir demek bu şekilde kullanıcı mecbur olarak bizim sitemize
        //girip formumuz üzerinden buton ile veri göndermesi gerekecektir.

        //VehicleMakes/CreateConfirmed
        [HttpPost]
        public IActionResult CreateConfirmed(VehicleMakeDto vehicleMakeDto)
        {
            var result = _vehicleMakeService.Create(vehicleMakeDto);

            if (result.IsSuccess)
            {
                //View yerine bu şekilde actionu bulunduğu sayfanın üst sayfası olan indexe yönlendirdim.
                return RedirectToAction("Index");
            }
            else
            {
                //TempData bir dictionary "ResultMessage" dictionarynin key i result.Message ise valuesi böylece
                //Create.cshtml de TempData kontrolü yaparım ve key var ise işlemleri yapar div olarak eklerim
                //Add metodu varsa message üzerine tekrar eklemiyor run time da hata veriyor
                //TempData.Add("ResultMessage", result.Message);
                //bu şekilde dictionary yapısıyla eğer varsa mesaj üzerine update ediyor yoksa ekliyor
                TempData["ResultMessage"] = result.Message;
                return RedirectToAction("Create");
            }
        }

    }
}
