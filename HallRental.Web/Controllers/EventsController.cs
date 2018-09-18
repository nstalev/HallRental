
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
        private readonly IHallsService hallsServices;
        private readonly UserManager<User> userManager;

        public EventsController(IEventsService eventsServices,
                                IHallsService hallsServices,
                                UserManager<User> userManager)
        {
            this.eventsServices = eventsServices;
            this.hallsServices = hallsServices;
            this.userManager = userManager;
        }




        public IActionResult DateCheck()
        {

            var vm = new DateCheckViewModel
            {
                Halls = this.hallsServices.AllHalls()
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

            Hall currentHall = this.hallsServices.GetHallById(dateCheckModel.HallId);
            DayOfWeek eventDateOfWeek = eventDate.DayOfWeek;

            decimal hallRentalPrice = dateCheckModel.TotalPrice;

            if (hallRentalPrice == 0)
            {
                hallRentalPrice = eventsServices.CheckHallStartPrice(currentHall, eventDateOfWeek, dateCheckModel.RentTime);

            }
            string hallName = currentHall.Name;

            string rentTimeDisplay = eventsServices.GetRentTimeDisplay(dateCheckModel.RentTime);



            var eventPriceModel = new EventPriceModel()
            {
                HallPrice = hallRentalPrice,
                TotalPrice = hallRentalPrice,
            };

            var priceCheckViewModel = new EventInfoAndPriceCheckViewModel()
            {
                Date = eventDate,
                RentTime = dateCheckModel.RentTime,
                HallId = dateCheckModel.HallId,
                HallName = hallName,
                RentTimeDisplay = rentTimeDisplay,
                HallRentalPrice = hallRentalPrice,
                TotalPrice = hallRentalPrice,
                SecurityGuardCostPerHour = currentHall.SecurityGuardCostPerHour,
                HallCapacity = currentHall.HallCapacity,
                ChairTableCostPerPerson = currentHall.TablesAndChairsCostPerPerson,
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

            Hall currentHall = this.hallsServices.GetHallById(summaryModel.HallId);
            string rentTimeDisplay = eventsServices.GetRentTimeDisplay(summaryModel.RentTime);

            User currentUser = await this.userManager.GetUserAsync(User);



            var summaryVM = new SummaryAndPerInfoVM()
            {
                HallId = summaryModel.HallId,
                HallName = currentHall.Name,
                Date = summaryModel.Date,
                RentTime = summaryModel.RentTime,
                RentTimeDisplay = rentTimeDisplay,
                EventEnd = summaryModel.EventEnd.ToString("hh:mm tt"),
                EventStart = summaryModel.EventStart.ToString("hh:mm tt"),
                EventTitle = summaryModel.EventTitle,
                NumberOfPeople = summaryModel.NumberOfPeople,
                UsingTablesAndChairs = summaryModel.UsingTablesAndChairs,
                TablesAndChairsCostPerPerson = currentHall.TablesAndChairsCostPerPerson,
                SecurityGuardCostPerHour = summaryModel.SecurityGuardCostPerHour,
                SecurityGuards = summaryModel.SecurityGuards,
                RequestedSecurityHoursPerGuard = summaryModel.RequestedSecurityHoursPerGuard,

                HallRentalPrice = summaryModel.HallRentalPrice,
                TablesAndChairsPrice = summaryModel.TablesAndChairsPrice,
                SecurityPrice = summaryModel.SecurityPrice,
                TotalPrice = summaryModel.TotalPrice,

                FullName = currentUser.FirstName + " " + currentUser.LastName,
                PhoneNumber = currentUser.PhoneNumber,
                Email = currentUser.Email

            };


            return View(summaryVM);
        }

        //[HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateEvent(CreateEventFormModel eventModel)
        {
            var hallExists = hallsServices.HallExists(eventModel.HallId);

            if (!hallExists)
            {

                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var summaryModel = new SummaryAndPerInfoVM
                {
                    Date = eventModel.Date,
                    HallId = eventModel.HallId,
                    RentTime = eventModel.RentTime,

                    NumberOfPeople = eventModel.NumberOfPeople,
                    UsingTablesAndChairs = eventModel.UsingTablesAndChairs,
                    EventTitle = eventModel.EventTitle,
                    HallRentalPrice = eventModel.HallRentalPrice,
                    SecurityGuardCostPerHour = eventModel.SecurityGuardCostPerHour,
                    SecurityGuards = eventModel.SecurityGuards,
                    RequestedSecurityHoursPerGuard = eventModel.RequestedSecurityHoursPerGuard,
                    SecurityPrice = eventModel.SecurityPrice,
                    TablesAndChairsPrice = eventModel.TablesAndChairsPrice,
                    TotalPrice = eventModel.TotalPrice,
                    FullName=  eventModel.FullName,
                    Email = eventModel.Email,
                    PhoneNumber = eventModel.PhoneNumber,
                    TablesAndChairsCostPerPerson = eventModel.TablesAndChairsCostPerPerson,
                    Caterer = eventModel.Caterer,
                    EventDescription = eventModel.EventDescription
                            
                };

                return View("Summary", summaryModel);
            }


            User currentUser = await this.userManager.GetUserAsync(User);

            this.eventsServices.Create(
                eventModel.HallId,
                currentUser.Id,
                eventModel.Date,
                eventModel.RentTime,
                eventModel.FullName,
                eventModel.Email,
                eventModel.PhoneNumber,
                eventModel.EventStart,
                eventModel.EventEnd,
                eventModel.NumberOfPeople,
                eventModel.UsingTablesAndChairs,
                eventModel.TablesAndChairsCostPerPerson,
                eventModel.SecurityGuardCostPerHour,
                eventModel.SecurityGuards,
                eventModel.RequestedSecurityHoursPerGuard,
                eventModel.HallRentalPrice,
                eventModel.TablesAndChairsPrice,
                eventModel.SecurityGuards,
                eventModel.TotalPrice,
                eventModel.EventDescription,
                eventModel.Caterer,
                eventModel.EventTitle);


            return RedirectToAction(nameof(ReservationSuccessful));
        }

        public IActionResult ReservationSuccessful()
        {
            return View();
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