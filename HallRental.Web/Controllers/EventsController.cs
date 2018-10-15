
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
            DateTime today = DateTime.UtcNow;

            var vm = new DateCheckViewModel
            {
                Halls = this.hallsServices.AllHalls()
                .Select(h => new SelectListItem
                {
                    Text = h.Name,
                    Value = h.Id.ToString(),
                    Selected = false
                }).ToList(),
                Date = null,
                CurrentDateTimeInMs = (long)today.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds,

            };


            return View(vm);
        }

        [Authorize]
        public IActionResult PriceCheck(DateCheckFormModel dateCheckModel)
        {
            DateTime today = DateTime.UtcNow;

            if (dateCheckModel.Date == null || dateCheckModel.HallId == 0)
            {
                TempData.AddErrorMessage("Please make sure all required fields are filled out correctly");
                return RedirectToAction(nameof(DateCheck), dateCheckModel);
            }

            DateTime eventDate = dateCheckModel.Date ?? DateTime.UtcNow;

            if (eventsServices.EventExists(eventDate, dateCheckModel.RentTime, dateCheckModel.HallId))
            {
                TempData.AddErrorMessage("The selected Hall and Date are already reserved");
                return RedirectToAction(nameof(DateCheck), dateCheckModel);
            }

            if (eventDate.Date < today.Date)
            {
                TempData.AddErrorMessage("You cannot make a reservation for a past date");
                return RedirectToAction(nameof(DateCheck), dateCheckModel);
            }

            Hall currentHall = this.hallsServices.GetHallById(dateCheckModel.HallId);
            DayOfWeek eventDateOfWeek = eventDate.DayOfWeek;

            decimal hallRentalPrice = dateCheckModel.TotalPrice;

            if (hallRentalPrice <= 0)
            {
                hallRentalPrice = eventsServices.CheckHallStartPrice(currentHall, eventDateOfWeek, dateCheckModel.RentTime);

            }
            string hallName = currentHall.Name;

            string rentTimeDisplay = eventsServices.GetRentTimeDisplay(dateCheckModel.RentTime);


            var startTime = eventsServices.GetStartTimeDefault(dateCheckModel.RentTime, eventDate);
            var endTime = eventsServices.GetEndTimeDefault(dateCheckModel.RentTime, eventDate);

            decimal securityDeposit = eventsServices.CalculateSecurityDeposit(dateCheckModel.RentTime, currentHall.SecurityDepositBefore10pm, currentHall.SecurityDepositAfter10pm);

            var eventPriceModel = new EventPriceModel()
            {
                HallPrice = hallRentalPrice,
                SecurityDeposit = securityDeposit,
                TotalPrice = hallRentalPrice + securityDeposit
            };

            var priceCheckViewModel = new EventInfoAndPriceCheckViewModel()
            {
                Date = eventDate,
                RentTime = dateCheckModel.RentTime,
                HallId = dateCheckModel.HallId,
                HallName = hallName,
                RentTimeDisplay = rentTimeDisplay,
                HallRentalPrice = hallRentalPrice,
                TotalPrice = hallRentalPrice + securityDeposit,
                SecurityGuardCostPerHour = currentHall.SecurityGuardCostPerHour,
                SecurityDepositBefore10pm = currentHall.SecurityDepositBefore10pm,
                SecurityDepositAfter10pm = currentHall.SecurityDepositAfter10pm,
                HallCapacity = currentHall.HallCapacity,
                ChairTableCostPerPerson = currentHall.TablesAndChairsCostPerPerson,
                EventPriceModel = eventPriceModel,
                EventStart = startTime,
                EventEnd = endTime,
                SecurityStartTime = startTime,
                SecurityEndTime = endTime,
                EventStartDateTimeInMs = (long)startTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds,
                EventEndDateTimeInMs = (long)endTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds,
                SecurityDeposit = securityDeposit
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

            if (!ModelState.IsValid)
            {
                DateCheckFormModel dateCheckModel = new DateCheckFormModel
                {
                    Date = summaryModel.Date,
                    HallId = summaryModel.HallId,
                    RentTime = summaryModel.RentTime,
                    TotalPrice = summaryModel.HallRentalPrice

                };

                return RedirectToAction("PriceCheck", dateCheckModel);
            }

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
                EventEnd = summaryModel.EventEnd,
                EventStart = summaryModel.EventStart,
                NumberOfPeople = summaryModel.NumberOfPeople,
                UsingTablesAndChairs = summaryModel.UsingTablesAndChairs,
                TablesAndChairsCostPerPerson = currentHall.TablesAndChairsCostPerPerson,
                SecurityGuardCostPerHour = summaryModel.SecurityGuardCostPerHour,
                ParkingLotSecurityService = summaryModel.ParkingLotSecurityService,
                ParkingLotSecurityHours = summaryModel.ParkingLotSecurityHours,
                SecurityStartTime = summaryModel.SecurityStartTime,
                SecurityEndTime = summaryModel.SecurityEndTime,

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
                    ParkingLotSecurityService = eventModel.ParkingLotSecurityService,
                    ParkingLotSecurityHours = eventModel.ParkingLotSecurityHours,
                    SecurityStartTime = eventModel.SecurityStartTime,
                    SecurityEndTime = eventModel.SecurityEndTime,
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
                eventModel.ParkingLotSecurityService,
                eventModel.ParkingLotSecurityHours,
                eventModel.SecurityStartTime,
                eventModel.SecurityEndTime,
                eventModel.HallRentalPrice,
                eventModel.TablesAndChairsPrice,
                eventModel.SecurityPrice,
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