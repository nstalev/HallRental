
namespace HallRental.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HallRental.Services;
    using HallRental.Services.Admin;
    using HallRental.Services.Admin.Models.Events;
    using HallRental.Web.Areas.Admin.Models.Events;
    using HallRental.Web.Infrastructure;
    using HallRental.Web.Services;
    using Microsoft.AspNetCore.Authorization;

    using Microsoft.AspNetCore.Mvc;
    [Area("Admin")]
    [Authorize(Roles = GlobalConstants.AdminRole)]
    public class EventsController : Controller
    {
        private readonly IEventsAdminService eventAdminService;
        private readonly IEventsService eventService;
        private readonly IHallsAdminService hallAdminService;
        private readonly IEmailSender emailSender;
        private const int pageSize = GlobalConstants.AdminEventsMaxPageSize;


        public EventsController(IEventsAdminService eventAdminService,
                                IEventsService eventService,
                                IHallsAdminService hallAdminService,
                                IEmailSender emailSender)
        {
            this.eventAdminService = eventAdminService;
            this.eventService = eventService;
            this.hallAdminService = hallAdminService;
            this.emailSender = emailSender;
        }

        public IActionResult EventRequests(string search, int page = 1)
        {

            if (string.IsNullOrEmpty(search))
            {
                search = "";
            }

            if (page <= 0)
            {
                page = 1;
            }

            DateTime currentDate = DateTime.Now.Date;

            int allEventRequstsCount = this.eventAdminService.TotalEvetRequests(search);

            IEnumerable<EventsListServiceModel> allEventRequstsSM = this.eventAdminService.GetEventRequests(search, page, pageSize);

            IEnumerable<EventsListModel> allEventRequsts = allEventRequstsSM.Select(e => new EventsListModel
            {
                EventId = e.Id,
                EventDate = e.EventDate,
                NumberOfPeople = e.NumberOfPeople,
                FullName = e.FullName,
                Email= e.Email,
                PhoneNumber = e.PhoneNumber,
                RentTimeDisplay = this.eventService.GetRentTimeDisplay(e.RentTime),
                HallName = e.HallName,
                IsReservationConfirmed = e.IsReservationConfirmed,
                Totalprice = e.Totalprice

            })
           .ToList();


            EventsViewModel vm = new EventsViewModel
            {
                Events = allEventRequsts,
                Search = search,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(allEventRequstsCount / (double)pageSize)
            };

            vm.TotalPages = vm.TotalPages == 0 ? 1 : vm.TotalPages;

            return View(vm);
        }


        public IActionResult ConfirmedEvents(string search, int page = 1)
        {
            if (string.IsNullOrEmpty(search))
            {
                search = "";
            }

            if (page <= 0)
            {
                page = 1;
            }

            DateTime currentDate = DateTime.Now.Date;


            int confirmedUpcomingEventsCount = this.eventAdminService.TotalConfirmedUpcomingEvents(search, currentDate);

            IEnumerable<EventsListServiceModel> confirmedUpcomingEventsSM = this.eventAdminService.GetConfirmedUpcomingEvents(search, page, pageSize, currentDate);

            IEnumerable<EventsListModel> confirmedUpcomingEvents = confirmedUpcomingEventsSM.Select(e => new EventsListModel
            {
                EventId = e.Id,
                EventDate = e.EventDate,
                NumberOfPeople = e.NumberOfPeople,
                FullName = e.FullName,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                RentTimeDisplay = this.eventService.GetRentTimeDisplay(e.RentTime),
                HallName = e.HallName,
                IsReservationConfirmed = e.IsReservationConfirmed,
                Totalprice = e.Totalprice

            })
           .ToList();


            EventsViewModel vm = new EventsViewModel
            {
                Events = confirmedUpcomingEvents,
                Search = search,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(confirmedUpcomingEventsCount / (double)pageSize)
            };

            vm.TotalPages = vm.TotalPages == 0 ? 1 : vm.TotalPages;

            return View(vm);
        }

        public IActionResult PassedEvents(string search, int page = 1)
        {
            if (string.IsNullOrEmpty(search))
            {
                search = "";
            }

            if (page <= 0)
            {
                page = 1;
            }

            DateTime currentDate = DateTime.Now.Date;

            int passedEventsCount = this.eventAdminService.TotalPassedEvents(search, currentDate);

            IEnumerable<EventsListServiceModel> passedEventsSM = this.eventAdminService.GetPassedEvents(search, page, pageSize, currentDate);

            IEnumerable<EventsListModel> passedEvents = passedEventsSM.Select(e => new EventsListModel
            {
                EventId = e.Id,
                EventDate = e.EventDate,
                NumberOfPeople = e.NumberOfPeople,
                FullName = e.FullName,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                RentTimeDisplay = this.eventService.GetRentTimeDisplay(e.RentTime),
                HallName = e.HallName,
                IsReservationConfirmed = e.IsReservationConfirmed,
                Totalprice = e.Totalprice

            })
           .ToList();


            EventsViewModel vm = new EventsViewModel
            {
                Events = passedEvents,
                Search = search,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(passedEventsCount / (double)pageSize)
            };

            vm.TotalPages = vm.TotalPages == 0 ? 1 : vm.TotalPages;

            return View(vm);
        }


        public IActionResult Details (int id)
        {
            bool eventExists = this.eventAdminService.EventExists(id);

            if (!eventExists)
            {
                TempData.AddErrorMessage("The event does not exist");
                return RedirectToAction(nameof(EventRequests));
            }

            EventDetailsAdminSM currentEvent = this.eventAdminService.EventById(id);

            currentEvent.RentTimeDisplay = this.eventService.GetRentTimeDisplay(currentEvent.RentTime);

            DateTime checkDate = currentEvent.EventDate;

            List<EvenAlertNotificationSM> notifications = this.eventAdminService.GetAllEventsOnTheSameDay(id, checkDate);

            currentEvent.EventNotifications = notifications;

            return View(currentEvent);
        }


        public async Task<IActionResult> ConfirmEvent(int id)
        {

            bool eventExists = this.eventAdminService.EventExists(id);

            if (!eventExists)
            {
                TempData.AddErrorMessage("The event does not exist");
                return RedirectToAction(nameof(EventRequests));
            }


            this.eventAdminService.ConfirmEvent(id);

            TempData.AddSuccessMessage($"Event ID {id} has been confirmed");


            //Send Email confirmation

            EventDetailsAdminSM currentEvent = this.eventAdminService.EventById(id);
            currentEvent.RentTimeDisplay = this.eventService.GetRentTimeDisplay(currentEvent.RentTime);
           
            string messageBody = this.eventAdminService.GetEmailConfirmationTextBody(currentEvent);

            await this.emailSender.SendEmailAsync(currentEvent.Email, "Reservation cunfirmed", messageBody);

            return RedirectToAction(nameof(ConfirmedEvents));
        }

        public IActionResult DisconfirmEvent(int id)
        {
            bool eventExists = this.eventAdminService.EventExists(id);

            if (!eventExists)
            {
                TempData.AddErrorMessage("The event does not exist");
                return RedirectToAction(nameof(EventRequests));
            }

            this.eventAdminService.DisConfirmEvent(id);

            TempData.AddErrorMessage($"Event ID {id} has been disconfirmed");

            return RedirectToAction(nameof(EventRequests));

        }


        public IActionResult DeleteEvent(int id)
        {
            bool eventExists = this.eventAdminService.EventExists(id);

            if (!eventExists)
            {
                TempData.AddErrorMessage("The event does not exist");
                return RedirectToAction(nameof(EventRequests));
            }

            this.eventAdminService.DeleteEvent(id);

            TempData.AddErrorMessage($"Event ID {id} has been Deleted");

            return RedirectToAction(nameof(EventRequests));

        }


        public IActionResult Edit(int id)
        {
            bool eventExists = this.eventAdminService.EventExists(id);

            if (!eventExists)
            {
                TempData.AddErrorMessage("The event does not exist");
                return RedirectToAction(nameof(EventRequests));
            }

            EditEventServiceModel vm = this.eventAdminService.GetEventByIdForEdit(id);

            return View(vm);
        }


        [HttpPost]
        public IActionResult Edit(int id, EditEventFormModel editFormModel)
        {
            bool eventExists = this.eventAdminService.EventExists(id);

            if (!eventExists)
            {
                TempData.AddErrorMessage("The event does not exist");
                return RedirectToAction(nameof(EventRequests));
            }

            if (!ModelState.IsValid)
            {
                EditEventServiceModel editServiceModel = GetEventEditServiceModel(editFormModel);
                return View(editServiceModel);
            }

            bool hallExists = this.hallAdminService.Exists(editFormModel.HallId);

            if (!hallExists)
            {
                EditEventServiceModel editServiceModel = GetEventEditServiceModel(editFormModel);
                TempData.AddErrorMessage($"The Hall with ID {editServiceModel.HallId} does not exist");
                return View(editServiceModel);
            }

            this.eventAdminService.EditEvent(id,
                                    editFormModel.Email,
                                    editFormModel.PhoneNumber,
                                    editFormModel.FullName,
                                    editFormModel.HallId,
                                    editFormModel.EventDate,
                                    editFormModel.RentTime,
                                    editFormModel.EventStart,
                                    editFormModel.EventEnd,
                                    editFormModel.NumberOfPeople,
                                    editFormModel.EventTitle,
                                    editFormModel.Description,
                                    editFormModel.Caterer,
                                    editFormModel.UsingTablesAndChairs,
                                    editFormModel.TablesAndChairsCostPerPerson,
                                    editFormModel.ParkingLotSecurityService,
                                    editFormModel.ParkingLotSecurityHours,
                                    editFormModel.SecurityStartTime,
                                    editFormModel.SecurityEndTime,
                                    editFormModel.SecurityGuardCostPerHour,
                                    editFormModel.AdditionalCharges,
                                    editFormModel.AdditionalChargesInformation,
                                    editFormModel.HallRentalPrice,
                                    editFormModel.TablesAndChairsPrice,
                                    editFormModel.ParkingLotSecurityPrice,
                                    editFormModel.SecurityDeposit,
                                    editFormModel.TotalPrice);


            TempData.AddSuccessMessage($"Event {editFormModel.Id} has been edited successfully");


            return RedirectToAction(nameof(EventRequests));
        }

        private EditEventServiceModel GetEventEditServiceModel(EditEventFormModel editFormModel)
        {
            return new EditEventServiceModel
            {
                Email = editFormModel.Email,
                PhoneNumber = editFormModel.PhoneNumber,
                FullName = editFormModel.FullName,
                HallId = editFormModel.HallId,
                EventStart = editFormModel.EventStart,
                EventEnd = editFormModel.EventEnd,
                NumberOfPeople = editFormModel.NumberOfPeople,
                EventTitle = editFormModel.EventTitle,
                Description = editFormModel.Description,
                Caterer = editFormModel.Caterer,
                UsingTablesAndChairs = editFormModel.UsingTablesAndChairs,
                TablesAndChairsCostPerPerson = editFormModel.TablesAndChairsCostPerPerson,
                ParkingLotSecurityService = editFormModel.ParkingLotSecurityService,
                ParkingLotSecurityHours = editFormModel.ParkingLotSecurityHours,
                SecurityStartTime = editFormModel.SecurityStartTime,
                SecurityEndTime = editFormModel.SecurityEndTime,
                SecurityGuardCostPerHour = editFormModel.SecurityGuardCostPerHour,
                AdditionalCharges = editFormModel.AdditionalCharges,
                AdditionalChargesInformation = editFormModel.AdditionalChargesInformation,
                HallRentalPrice = editFormModel.HallRentalPrice,
                TablesAndChairsPrice = editFormModel.TablesAndChairsPrice,
                ParkingLotSecurityPrice = editFormModel.ParkingLotSecurityPrice,
                SecurityDeposit = editFormModel.SecurityDeposit,
                TotalPrice = editFormModel.TotalPrice
            };
        }
    }
}