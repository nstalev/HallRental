
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
    using HallRental.Data.Models;
    using HallRental.Data.Enums;
    using static HallRental.Data.Enums.Enums;

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
                               eventModel.TotalPrice,
                               eventModel.SecurityGuards,
                               eventModel.WithCHairsAndTable
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
                return RedirectToAction(nameof(DateCheck), dateCheckModel);
            }

            DateTime eventDate = dateCheckModel.Date ?? DateTime.Now;

            if (events.EventExists(eventDate, dateCheckModel.RentTime, dateCheckModel.HallId))
            {
                TempData.AddErrorMessage("The selected Hall and Date are already reserved");
                return RedirectToAction(nameof(DateCheck), dateCheckModel);
            }

            Hall currentHall = this.halls.GetHallById(dateCheckModel.HallId);
            DayOfWeek eventDateOfWeek = eventDate.DayOfWeek;

            decimal startPrice = dateCheckModel.TotalPrice;

            if (startPrice == 0)
            {
                startPrice = CheckHallStartPrice(currentHall, eventDateOfWeek, dateCheckModel.RentTime);

            }
            string hallName = currentHall.Name;

            string rentTimeDisplay = dateCheckModel.RentTime.ToString();

            var priceCheckViewModel = new EventInfoAndPriceCheckViewModel()
            {
                Date = eventDate,
                RentTime = dateCheckModel.RentTime,
                HallId = dateCheckModel.HallId,
                HallName = hallName,
                RentTimeDisplay = rentTimeDisplay,
                Price = startPrice,
                SecurityGuardCostPerHour = currentHall.SecurityGuardCostPerHour,
                HallCapacity = currentHall.HallCapacity,
                ChairTableCostPerPerson = currentHall.ChairTablePerPersonCost
               
            };

            return View(priceCheckViewModel);
        }


        public IActionResult UpdatePriceView (EventPriceModel model)
        {

            return PartialView("_PartialPrice", model.TotalPrice);
        }



        public IActionResult PersonalInfo(PersonalInformationViewModel personalInfo)
        {


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






        private decimal CheckHallStartPrice(Hall currentHall, DayOfWeek eventDate, RentTimeEnum rentTime)
        {

            if (rentTime == RentTimeEnum.EightAMtoThreePM)
            {
                if (eventDate == DayOfWeek.Monday 
                   || eventDate == DayOfWeek.Tuesday
                   || eventDate == DayOfWeek.Wednesday
                   || eventDate == DayOfWeek.Thursday
                   || eventDate == DayOfWeek.Friday)
                {
                    return currentHall.MondayFriday8amTo3pm;
                }
                else if (eventDate == DayOfWeek.Saturday)
                {
                    return currentHall.Saturday8amTo3pm;
                }
                else if (eventDate == DayOfWeek.Sunday)
                {
                    return currentHall.Sunday8amTo3pm;
                }
            }
            else if (rentTime == RentTimeEnum.FourPMtoMidNight)
            {
                if (eventDate == DayOfWeek.Monday
                   || eventDate == DayOfWeek.Tuesday
                   || eventDate == DayOfWeek.Wednesday
                   || eventDate == DayOfWeek.Thursday)
                {
                    return currentHall.MondayThursday4pmToMN;
                }
                else if (eventDate == DayOfWeek.Friday)
                {
                    return currentHall.Friday4pmToMN;
                }
                else if (eventDate == DayOfWeek.Saturday)
                {
                    return currentHall.Saturday4pmToMN;
                }
                else if (eventDate == DayOfWeek.Sunday)
                {
                    return currentHall.Sunday4pmToMN;
                }
            }
            else if (rentTime == RentTimeEnum.AllDay)
            {
                if (eventDate == DayOfWeek.Monday
                   || eventDate == DayOfWeek.Tuesday
                   || eventDate == DayOfWeek.Wednesday
                   || eventDate == DayOfWeek.Thursday)
                {
                    return currentHall.MondayFriday8amTo3pm + currentHall.MondayThursday4pmToMN;
                }
                else if (eventDate == DayOfWeek.Friday)
                {
                    return currentHall.MondayFriday8amTo3pm + currentHall.Friday4pmToMN;
                }
                else if (eventDate == DayOfWeek.Saturday)
                {
                    return currentHall.Saturday8amTo3pm + currentHall.Saturday4pmToMN;
                }
                else if (eventDate == DayOfWeek.Sunday)
                {
                    return currentHall.Sunday8amTo3pm + currentHall.Sunday4pmToMN;
                }
            }

            return 0;
        }


    }
}