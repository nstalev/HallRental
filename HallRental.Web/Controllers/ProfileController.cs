using System;
using System.Collections.Generic;
using System.Linq;
using HallRental.Data.Models;
using HallRental.Services;
using HallRental.Services.Models.Profile;
using HallRental.Web.Infrastructure;
using HallRental.Web.Models.ProfileViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HallRental.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IProfileService profileService;
        private readonly IEventsService eventService;

        private const int pageSize = GlobalConstants.MyEventsMaxPageSize;

        public ProfileController(UserManager<User> userManager,
                                IProfileService profileService,
                                IEventsService eventService)
        {
            this.userManager = userManager;
            this.profileService = profileService;
            this.eventService = eventService;
        }



        public IActionResult Index()
        {
            return View();
        }


        public IActionResult MyReservations(int page = 1)
        {
            if (page <= 0)
            {
                page = 1;
            }

            string currentUserId =  this.userManager.GetUserId(User);

            DateTime markDate = DateTime.Now.Date;

            var myEventsList = this.profileService.MyFutureEvents(currentUserId, page, pageSize, markDate);

            int myEventsCount = this.profileService.TotalFutureEvents(currentUserId, markDate);

            IEnumerable<MyEventsListModel> myEvents = myEventsList.Select(e => new MyEventsListModel
            {
                 EventId = e.Id,
                 EventDate = e.EventDate,
                 NumberOfPeople =e.NumberOfPeople,
                 EventTitle = e.EventTitle,
                 RentTimeDisplay = this.eventService.GetRentTimeDisplay(e.RentTime),
                 HallName = e.HallName,
                 IsConfirmed = e.IsReservationConfirmed,
                 Totalprice = e.Totalprice
                 
            })
            .ToList();

            var vm = new MyEventsViewModel
            {
                Events = myEvents,
                CurrentPage= page,
                TotalPages = (int)Math.Ceiling(myEventsCount / (double)pageSize)
            };

            vm.TotalPages = vm.TotalPages == 0 ? 1 : vm.TotalPages;


            return View(vm);
        }


        public IActionResult PassedEvents(int page = 1)
        {
            if (page <= 0)
            {
                page = 1;
            }

            DateTime markDate = DateTime.Now.Date;

            string currentUserId = this.userManager.GetUserId(User);

            var myEventsList = this.profileService.MyPassedEvents(currentUserId, page, pageSize, markDate);

            int myEventsCount = this.profileService.TotalPassedEvents(currentUserId, markDate);

            IEnumerable<MyEventsListModel> myEvents = myEventsList.Select(e => new MyEventsListModel
            {
                EventId = e.Id,
                EventDate = e.EventDate,
                NumberOfPeople = e.NumberOfPeople,
                EventTitle = e.EventTitle,
                RentTimeDisplay = this.eventService.GetRentTimeDisplay(e.RentTime),
                HallName = e.HallName,
                IsConfirmed = e.IsReservationConfirmed,
                Totalprice = e.Totalprice

            })
            .ToList();

            var vm = new MyEventsViewModel
            {
                Events = myEvents,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(myEventsCount / (double)pageSize)
            };

            vm.TotalPages = vm.TotalPages == 0 ? 1 : vm.TotalPages;

            return View(vm);
        }



        public IActionResult EventDetails(int id)
        {

            string currentUserId = this.userManager.GetUserId(User);

            bool isEventExists = this.eventService.CheckIfEventExists(id);

            if (!isEventExists)
            {
                TempData.AddErrorMessage("The event does not exists");
                return RedirectToAction(nameof(MyReservations));
            }

            EventDetailsServiceModel currentEvent = this.profileService.EventById(id);

            if (currentUserId != currentEvent.TenantId)
            {
                return Forbid();
            }

            currentEvent.RentTimeDisplay = this.eventService.GetRentTimeDisplay(currentEvent.RentTime);

            return View(currentEvent);
        }
    }
}