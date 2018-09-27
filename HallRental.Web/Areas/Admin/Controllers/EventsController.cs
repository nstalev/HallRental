
namespace HallRental.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HallRental.Services;
    using HallRental.Services.Admin;
    using HallRental.Services.Admin.Models.Events;
    using HallRental.Services.Models.Profile;
    using HallRental.Web.Areas.Admin.Models.Events;
    using HallRental.Web.Infrastructure;
    using Microsoft.AspNetCore.Authorization;

    using Microsoft.AspNetCore.Mvc;
    [Area("Admin")]
    [Authorize(Roles = GlobalConstants.AdminRole)]
    public class EventsController : Controller
    {
        private readonly IEventsAdminService eventAdminService;
        private readonly IEventsService eventService;
        private const int pageSize = GlobalConstants.AdminEventsMaxPageSize;


        public EventsController(IEventsAdminService eventAdminService,
                                IEventsService eventService)
        {
            this.eventAdminService = eventAdminService;
            this.eventService = eventService;
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

            EventDetailsServiceModel currentEvent = this.eventAdminService.EventById(id);

            currentEvent.RentTimeDisplay = this.eventService.GetRentTimeDisplay(currentEvent.RentTime);

            return View(currentEvent);
        }


        public IActionResult ConfirmEvent(int id)
        {

            bool eventExists = this.eventAdminService.EventExists(id);

            if (!eventExists)
            {
                return RedirectToAction("Index", "Home");
            }

            this.eventAdminService.ConfirmEvent(id);


            return RedirectToAction(nameof(ConfirmedEvents));
        }
    }
}