
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
    using HallRental.Web.Services;
    using static HallRental.Data.Enums.Enums;

    public class EventsController : Controller
    {
        private readonly IEventsService eventsServices;
        private readonly IHallsService hallsServices;
        private readonly UserManager<User> userManager;
        private readonly IEmailSender emailSender;

        public EventsController(IEventsService eventsServices,
                                IHallsService hallsServices,
                                UserManager<User> userManager,
                                IEmailSender emailSender)
        {
            this.eventsServices = eventsServices;
            this.hallsServices = hallsServices;
            this.userManager = userManager;
            this.emailSender = emailSender;
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
                CurrentDateTime = today,
                HallsPriceList = this.hallsServices.AllHallsPriceList()
            };


            return View(vm);
        }

        
        public IActionResult PriceCheck(DateCheckFormModel dateCheckModel)
        {
            DateTime today = DateTime.UtcNow;

            var hallExists = this.hallsServices.HallExists(dateCheckModel.HallId);

              var enumExists = Enum.IsDefined(typeof(RentTimeEnum), dateCheckModel.RentTime);

            if (dateCheckModel.Date == null || !hallExists || !enumExists)
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

            var priceCheckViewModel = GetEventInfoAndPriceCheckViewModel(dateCheckModel, eventDate);


            return View(priceCheckViewModel);
        }

        [Authorize]
        public IActionResult PriceCheckAuthorize(DateCheckFormModel dateCheckModel)
        {
            DateTime today = DateTime.UtcNow;

            var hallExists = this.hallsServices.HallExists(dateCheckModel.HallId);

            var enumExists = Enum.IsDefined(typeof(RentTimeEnum), dateCheckModel.RentTime);

            if (dateCheckModel.Date == null || !hallExists || !enumExists)
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

            var priceCheckViewModel = GetEventInfoAndPriceCheckViewModel(dateCheckModel, eventDate);


            return View(priceCheckViewModel);

        }


        public IActionResult UpdatePriceView(EventPriceModel priceModel)
        {
            return PartialView("_PartialPrice", priceModel);
        }


        public async Task<IActionResult> Summary(SummaryAndPersonalInfoModel summaryModel)
        {

            if (!ModelState.IsValid)
            {
                var dateCheckModel = GetDateCheckFormModel(summaryModel);

                TempData.AddErrorMessage("Invalid input. Please fill in all required fields");
                return RedirectToAction("PriceCheck", dateCheckModel);
            }

            if (summaryModel.HallId == 0 || summaryModel.Date == null)
            {
                var dateCheckModel = GetDateCheckFormModel(summaryModel);

                TempData.AddErrorMessage("Invalid input. Please fill in all required fields");
                return RedirectToAction("PriceCheck", dateCheckModel);
            }

            if (summaryModel.ParkingLotSecurityService == true 
                && summaryModel.ParkingLotSecurityPrice <= 0)
            {
                var dateCheckModel = GetDateCheckFormModel(summaryModel);

                TempData.AddErrorMessage("Invalid input. Please fill in all required fields");
                return RedirectToAction("PriceCheck", dateCheckModel);
            }

            if (summaryModel.UsingTablesAndChairs == true
               && summaryModel.TablesAndChairsPrice <= 0
               && summaryModel.TablesAndChairsCostPerPerson > 0)
            {
                var dateCheckModel = GetDateCheckFormModel(summaryModel);

                TempData.AddErrorMessage("Invalid input. Please fill in all required fields");
                return RedirectToAction("PriceCheck", dateCheckModel);
            }

            Hall currentHall = this.hallsServices.GetHallById(summaryModel.HallId);
            string rentTimeDisplay = eventsServices.GetRentTimeDisplay(summaryModel.RentTime);

           

            decimal securityDeposit = eventsServices.CalculateSecurityDeposit(summaryModel.RentTime, summaryModel.EventEnd, summaryModel.Date,  currentHall.SecurityDepositBefore10pm, currentHall.SecurityDepositAfter10pm);


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
                SecurityCostPerHour = summaryModel.SecurityCostPerHour,
                ParkingLotSecurityService = summaryModel.ParkingLotSecurityService,
                ParkingLotSecurityHours = summaryModel.ParkingLotSecurityHours,
                SecurityStartTime = summaryModel.SecurityStartTime,
                SecurityEndTime = summaryModel.SecurityEndTime,

                HallRentalPrice = summaryModel.HallRentalPrice,
                TablesAndChairsPrice = summaryModel.TablesAndChairsPrice,
                ParkingLotSecurityPrice = summaryModel.ParkingLotSecurityPrice,
                SecurityDeposit = securityDeposit,
                TotalPrice = summaryModel.TotalPrice,


            };

            User currentUser = await this.userManager.GetUserAsync(User);

            if (currentUser != null)
            {
                summaryVM.FullName = currentUser.FirstName + " " + currentUser.LastName;
                summaryVM.PhoneNumber = currentUser.PhoneNumber;
                summaryVM.Email = currentUser.Email;
            }


            return View(summaryVM);
        }

        public async Task<IActionResult> CreateEvent(CreateEventFormModel eventModel)
        {
            var hallExists = hallsServices.HallExists(eventModel.HallId);

            if (!hallExists)
            {

                return NotFound();
            }



            if (!ModelState.IsValid)
            {
                string rentTimeDisplay = eventsServices.GetRentTimeDisplay(eventModel.RentTime);

                var summaryModel = new SummaryAndPerInfoVM
                {
                    Date = eventModel.Date,
                    HallId = eventModel.HallId,
                    RentTime = eventModel.RentTime,
                    RentTimeDisplay = rentTimeDisplay,
                    NumberOfPeople = eventModel.NumberOfPeople,
                    UsingTablesAndChairs = eventModel.UsingTablesAndChairs,
                    TypeOfEvent = eventModel.TypeOfEvent,
                    HallRentalPrice = eventModel.HallRentalPrice,
                    SecurityCostPerHour = eventModel.SecurityCostPerHour,
                    ParkingLotSecurityService = eventModel.ParkingLotSecurityService,
                    ParkingLotSecurityHours = eventModel.ParkingLotSecurityHours,
                    SecurityStartTime = eventModel.SecurityStartTime,
                    SecurityEndTime = eventModel.SecurityEndTime,
                    ParkingLotSecurityPrice = eventModel.ParkingLotSecurityPrice,
                    TablesAndChairsPrice = eventModel.TablesAndChairsPrice,
                    TotalPrice = eventModel.TotalPrice,
                    FullName = eventModel.FullName,
                    Email = eventModel.Email,
                    PhoneNumber = eventModel.PhoneNumber,
                    TablesAndChairsCostPerPerson = eventModel.TablesAndChairsCostPerPerson,
                    Caterer = eventModel.Caterer,
                    EventDescription = eventModel.EventDescription

                };

                return View("Summary", summaryModel);
            }


            string tenantId = null;

            User currentUser = await this.userManager.GetUserAsync(User);

            if (currentUser != null)
            {
                tenantId = currentUser.Id;
            }

            this.eventsServices.Create(
                eventModel.HallId,
                tenantId,
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
                eventModel.SecurityCostPerHour,
                eventModel.ParkingLotSecurityService,
                eventModel.ParkingLotSecurityHours,
                eventModel.SecurityStartTime,
                eventModel.SecurityEndTime,
                eventModel.HallRentalPrice,
                eventModel.TablesAndChairsPrice,
                eventModel.ParkingLotSecurityPrice,
                eventModel.SecurityDeposit,
                eventModel.TotalPrice,
                eventModel.EventDescription,
                eventModel.Caterer,
                eventModel.TypeOfEvent);


            //Send Email to Administration 

            string messageBody = this.eventsServices.GetTextBodyForEmailForReservation(
                                                                    eventModel.Date,
                                                                    eventModel.FullName,
                                                                    eventModel.Email,
                                                                    eventModel.PhoneNumber,
                                                                    eventModel.NumberOfPeople,
                                                                    eventModel.TotalPrice);

            string messageBodyForTenant = this.eventsServices.GetTextBodyForTenant(
                                                                   eventModel.Date,
                                                                   eventModel.FullName,
                                                                   eventModel.NumberOfPeople,
                                                                   eventModel.TotalPrice);

            await this.emailSender.SendEmailAsync(GlobalConstants.HomeEmail, "Reservation request", messageBody);

            await this.emailSender.SendEmailAsync(eventModel.Email, "Reservation request received", messageBodyForTenant);


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




        public DateCheckFormModel GetDateCheckFormModel(SummaryAndPersonalInfoModel summaryModel)
        {
            return new DateCheckFormModel
            {
                Date = summaryModel.Date,
                HallId = summaryModel.HallId,
                RentTime = summaryModel.RentTime,
                TotalPrice = summaryModel.HallRentalPrice
            };
        }



        private EventInfoAndPriceCheckViewModel GetEventInfoAndPriceCheckViewModel(DateCheckFormModel dateCheckModel, DateTime eventDate)
        {

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


            var eventPriceModel = new EventPriceModel()
            {
                HallPrice = hallRentalPrice,
                TotalPrice = hallRentalPrice
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
                SecurityCostPerHour = currentHall.SecurityGuardCostPerHour,
                HallCapacity = currentHall.HallCapacity,
                TablesAndChairsCostPerPerson = currentHall.TablesAndChairsCostPerPerson,
                EventPriceModel = eventPriceModel,
                EventStart = startTime,
                EventEnd = endTime,
                SecurityStartTime = startTime,
                SecurityEndTime = endTime
            };

            return priceCheckViewModel;
        }

    }
}