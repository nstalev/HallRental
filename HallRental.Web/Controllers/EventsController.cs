
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

            if (eventModel.EventDate < DateTime.Now)
            {
                ModelState.AddModelError("EventStart", "Event Start Date cannot be before DateTime Now");
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
                               eventModel.EventDate,
                               eventModel.RentTime,
                               eventModel.NumberOfPeople
                               );



            return RedirectToAction("Index", "Calendar");
        }



        public IActionResult DateCheck()
        {

            //var vm = new DateCheckFormModel();
            //vm.RentTime = RentTimeEnum.eightAMtoThreePM;

            return View();
        }
    }
}