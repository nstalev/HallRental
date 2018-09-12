
namespace HallRental.Web.Controllers
{
    using HallRental.Services;
    using HallRental.Web.Infrastructure;
    using HallRental.Web.Models.EventsModel;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Linq;
    using HallRental.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public class EventsController : Controller
    {
        private readonly IEventsService eventsServices;
        private readonly IHallsService halls;
        private readonly UserManager<User> userManager;

        public EventsController(IEventsService eventsServices,
                                IHallsService halls,
                                UserManager<User> userManager)
        {
            this.eventsServices = eventsServices;
            this.halls = halls;
            this.userManager = userManager;
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


            this.eventsServices.Create(eventModel.Email,
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

        [Authorize]
        public IActionResult PriceCheck(DateCheckFormModel dateCheckModel)
        {

            if (dateCheckModel.Date == null || dateCheckModel.HallId == 0)
            {
                TempData.AddErrorMessage("Please make sure all required fields are filled out correctly");
                return RedirectToAction(nameof(DateCheck), dateCheckModel);
            }

            DateTime eventDate = dateCheckModel.Date ?? DateTime.Now;

            if (eventsServices.EventExists(eventDate, dateCheckModel.RentTime, dateCheckModel.HallId))
            {
                TempData.AddErrorMessage("The selected Hall and Date are already reserved");
                return RedirectToAction(nameof(DateCheck), dateCheckModel);
            }

            Hall currentHall = this.halls.GetHallById(dateCheckModel.HallId);
            DayOfWeek eventDateOfWeek = eventDate.DayOfWeek;

            decimal hallRentPrice = dateCheckModel.TotalPrice;

            if (hallRentPrice == 0)
            {
                hallRentPrice = eventsServices.CheckHallStartPrice(currentHall, eventDateOfWeek, dateCheckModel.RentTime);

            }
            string hallName = currentHall.Name;

            string rentTimeDisplay = eventsServices.GetRentTimeDisplay(dateCheckModel.RentTime);



            var eventPriceModel = new EventPriceModel()
            {
                HallPrice = hallRentPrice,
                TotalPrice = hallRentPrice,
            };

            var priceCheckViewModel = new EventInfoAndPriceCheckViewModel()
            {
                Date = eventDate,
                RentTime = dateCheckModel.RentTime,
                HallId = dateCheckModel.HallId,
                HallName = hallName,
                RentTimeDisplay = rentTimeDisplay,
                HallRentPrice = hallRentPrice,
                TotalPrice = hallRentPrice,
                SecurityGuardCostPerHour = currentHall.SecurityGuardCostPerHour,
                HallCapacity = currentHall.HallCapacity,
                ChairTableCostPerPerson = currentHall.ChairTablePerPersonCost,
                EventPriceModel = eventPriceModel,
                EventStart = eventsServices.GetStartTimeDefault(dateCheckModel.RentTime, eventDate),
                EventEnd = eventsServices.GetEndTimeDefault(dateCheckModel.RentTime, eventDate)

            };

            return View(priceCheckViewModel);
        }


        [Authorize]
        public IActionResult UpdatePriceView(EventPriceModel priceModel)
        {

            return PartialView("_PartialPrice", priceModel);
        }


        [Authorize]
        public async Task<IActionResult> Summary(SummaryAndPersonalInfoModel summaryModel)
        {

            if (summaryModel.HallId == 0 || summaryModel.Date == null)
            {
                return NotFound();
            }

            Hall currentHall = this.halls.GetHallById(summaryModel.HallId);
            string rentTimeDisplay = eventsServices.GetRentTimeDisplay(summaryModel.RentTime);

            User currentUser = await this.userManager.GetUserAsync(User);

            var userName = currentUser.UserName;


            var summaryVM = new SummaryAndPerInfoVM()
            {
                HallId = summaryModel.HallId,
                HallName = currentHall.Name,
                Date = summaryModel.Date,
                RentTime = summaryModel.RentTime,
                RentTimeDisplay = rentTimeDisplay,
                EventEnd = summaryModel.EventEnd.ToString("t"),
                EventStart = summaryModel.EventStart.ToString("t"),
                EventTitle = summaryModel.EventTitle,
                NumberOfPeople = summaryModel.NumberOfPeople,
                SecurityGuardCostPerHour = summaryModel.SecurityGuardCostPerHour,
                SecurityGuards = summaryModel.SecurityGuards,
                RequestedSecurityHoursPerGuard = summaryModel.SecurityServiceHoursPerGuard,

                HallRentPrice = summaryModel.HallRentPrice,
                TablesAndChairsPrice = summaryModel.TablesAndChairsPrice,
                SecurityPrice = summaryModel.SecurityPrice,
                TotalPrice = summaryModel.TotalPrice

            };


            return View(summaryVM);
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateEvent(CreateEventFormModel eventModel)
        {
            if (!ModelState.IsValid)
            {
                return View(eventModel);
            }


            return RedirectToAction("Index", "Calendar");
        }




        public JsonResult CheckCurrentDate(DateCheckJsonModel dateModel)
        {
            var eventDate = new EventDateModel();

            var eventExists = eventsServices.EventExists(dateModel.Date, dateModel.RentTime, dateModel.HallId);

            if (eventExists)
            {
                eventDate.EventExists = true;
            }

            return Json(eventDate);
        }

    }
}