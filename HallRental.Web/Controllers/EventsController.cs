
namespace HallRental.Web.Controllers
{
    using HallRental.Services;
    using HallRental.Web.Infrastructure;
    using HallRental.Web.Models.EventsModel;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class EventsController : Controller
    {
        private readonly IEventsService events;
        private readonly IHallsService halls;

        public EventsController(IEventsService events,
                                IHallsService halls)
        {
            this.events = events;
            this.halls = halls;
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
                               eventModel.NumberOfPeople,
                               eventModel.TotalPrice
                               );

            TempData.AddSuccessMessage("Your event request has been successfully submitted");

            return RedirectToAction("Index", "Calendar");
        }


        public IActionResult DateCheck()
        {

            var vm = new DateCheckViewModel
            {
                Halls = this.halls.AllHalls()
                .Select(h => new SelectListItem
                {
                    Text = h.Name,
                    Value = h.Id.ToString(),
                    Selected = false
                }).ToList(),
                Date = null
            };


            return View(vm);
        }



        public IActionResult PriceCheck(DateCheckFormModel dateCheckModel)
        {
           
            if (dateCheckModel.Date == null || dateCheckModel.HallId == 0)
            {
                TempData.AddErrorMessage("Please make sure all required fields are filled out correctly");
              return  RedirectToAction(nameof(DateCheck), dateCheckModel);
            }

            DateTime date = dateCheckModel.Date ?? DateTime.Now;

            if (events.EventExists(date, dateCheckModel.RentTime, dateCheckModel.HallId))
            {
                TempData.AddErrorMessage("The selected Hall and Date are already reserved");
                return RedirectToAction(nameof(DateCheck), dateCheckModel);
            }

            return View();
        }




        public JsonResult CheckCurrentDate(DateCheckJsonModel dateModel)
        {
            var eventDate = new EventDateModel();

            var eventExists = events.EventExists(dateModel.Date, dateModel.RentTime, dateModel.HallId);

            if (eventExists)
            {

                eventDate.EventExists = true;
            }
          
            

            return Json(eventDate);
        }
    }
}