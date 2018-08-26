
namespace HallRental.Web.Controllers
{
    using HallRental.Services;
    using HallRental.Services.Models.Events;
    using HallRental.Web.Models.CalendarModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public class CalendarController : Controller
    {

        private readonly ICalendarService calendarService;
        private readonly IHallsService hallsService;

        public CalendarController(ICalendarService calendarService,
                                IHallsService hallsService)
        {
            this.calendarService = calendarService;
            this.hallsService = hallsService;
        }

        public IActionResult Index(int hallId)
        {
            var halls = this.hallsService.AllHalls();

            if (hallId == 0)
            {
                hallId = halls.First().Id;

            }


            CalendarViewModel vm = new CalendarViewModel()
            {
                AllHals = halls,
                CurrentHallId = hallId
            };

            return View(vm);
        }


        public JsonResult GetEvents(CalendarHallJsonModel hallModel)
        {
            var result = new List<EventCalendarServiceModel>();

            result.AddRange(this.calendarService.AllEvents(hallModel.HallId));
           // var allEvents = this.calendarService.AllEvents(hallModel.HallId);

            return Json(result);
        }

    }
}