using McTours.Business.Services;
using McTours.Tickets;
using McTours.WebApp.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace McTours.WebApp.Controllers
{
    public class TicketsController : Controller
    {
        private readonly PassengerService _passengerService = new PassengerService();
        private readonly TicketService _ticketService = new TicketService();
        private readonly BusTripService _busTripService = new BusTripService();
        public IActionResult Index()
        {
            var ticket = _ticketService.GetAll();
            return View(ticket);
        }

        [HttpPost]
        public IActionResult BusTripTicketCreate(int busTripId, int seatNumber, int passengerId)
        {
            var busTrip = _busTripService.GetById(busTripId);
            var passenger = _passengerService.GetById(passengerId);
            var ticketReview = new TicketReview()
            {
                BusTripId = busTrip.Id,
                PassengerId = passenger.Id,
                PassengerFirstName = passenger.FirstName,
                PassengerLastName = passenger.LastName,
                Price = busTrip.TicketPrice,
                SeatNumber = seatNumber
            };
            return PartialView(ticketReview);
        }

        public IActionResult Create()
        {
            ViewBag.Tickets = _ticketService.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(TicketDto ticket)
        {
            var result = _ticketService.Create(ticket);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("Tickets", "BusTrips", new {id=ticket.BusTripId});
            }
            else
            {
                ViewData["ErrorMessage"] = result.Message;
                return View(ticket);
            }
        }
        public IActionResult Update(int id)
        {
            var ticket = _ticketService.GetById(id);
            if (ticket != null)
            {
                ViewBag.Tickets = _ticketService.GetAll();
                return View(ticket);
            }
            else
            {
                ViewData["ErrorMessage"] = $"{id} ID'li kayıt Bulunamadı";
                ViewBag.Tickets = _ticketService.GetAll();
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Update(TicketDto ticket)
        {
            var result = _ticketService.Update(ticket);

            if (result.IsSuccess)
            {
                TempData[Keys.SuccessMessage] = result.Message;
                return RedirectToAction("Index");
            }
            else
            {
                ViewData[Keys.ErrorMessage] = result.Message;
                ViewBag.Tickets = _ticketService.GetAll();
                return View(ticket);
            }
        }
        [HttpPost]
        public IActionResult Delete(TicketDto ticket)
        {
            var result = _ticketService.Delete(ticket);

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
