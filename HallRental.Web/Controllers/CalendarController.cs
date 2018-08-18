
namespace HallRental.Web.Controllers
{
    using HallRental.Services;
    using Microsoft.AspNetCore.Mvc;

    public class CalendarController : Controller
    {

        private readonly ICalendarService calendarService;

        public CalendarController(ICalendarService calendarService)
        {
            this.calendarService = calendarService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public JsonResult GetEvents()
        {
            var allEvents = this.calendarService.AllEvents();

            return Json(allEvents);
        }

    }
}