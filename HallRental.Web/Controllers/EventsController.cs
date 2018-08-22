
namespace HallRental.Web.Controllers
{
    using HallRental.Services;
    using HallRental.Services.Models.Events;
    using HallRental.Web.Infrastructure;
    using HallRental.Web.Models.EventsModel;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;

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

            TempData.AddSuccessMessage("Your event request has been successfully submitted");

            return RedirectToAction("Index", "Calendar");
        }



        public IActionResult DateCheck()
        {

            //var vm = new DateCheckFormModel();
            //vm.RentTime = RentTimeEnum.eightAMtoThreePM;


            return View();
        }



        public IActionResult PriceCheck(DateCheckFormModel dateCheckModel)
        {
           
            if (dateCheckModel.Date == DateTime.MinValue)
            {
                TempData.AddErrorMessage("Please make sure all required fields are filled out correctly");
              return  RedirectToAction(nameof(DateCheck), dateCheckModel);
            }

            return View();
        }

        public JsonResult CheckCurrentDate(DateCheckJsonModel dateModel)
        {
            var eventDate = new EventDateModel();

            var eventExists = events.EventExists(dateModel.Date, dateModel.RentTime);

            if (eventExists)
            {

                eventDate.EventExists = true;
            }
          
            

            return Json(eventDate);
        }
    }
}