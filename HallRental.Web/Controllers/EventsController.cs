
namespace HallRental.Web.Controllers
{
    using HallRental.Services;
    using HallRental.Web.Models.EventsModel;
    using Microsoft.AspNetCore.Mvc;
    using System;

    public class EventsController : Controller
    {
        private readonly IEventsService events;

        public EventsController(IEventsService events)
        {
            this.events = events;
        }

        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EventFormModel eventModel)
        {

            if (eventModel.EventStart < DateTime.Now)
            {
                ModelState.AddModelError("EventStart", "Event Start Date cannot be before DateTime Now");
                return View(eventModel);
            }

            if (eventModel.EventStart >= eventModel.EventEdn)
            {
                ModelState.AddModelError("EventStart", "Event Start Date cannot be after Event Edn Date");
                return View(eventModel);
            }

            if (!ModelState.IsValid)
            {
                return View(eventModel);
            }


            this.events.Create(eventModel.Email,
                               eventModel.PhoneNumber,
                               eventModel.Description,
                               eventModel.EventTitle,
                               eventModel.EventStart,
                               eventModel.EventEdn,
                               eventModel.NumberOfPeople
                               );



            return RedirectToAction("Index", "Calendar");
        }
    }
}