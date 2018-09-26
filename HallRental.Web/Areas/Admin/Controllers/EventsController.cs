
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
                EventTitle = e.EventTitle,
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


            return View();
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



            return View();
        }
    }
}