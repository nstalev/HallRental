using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HallRental.Data.Models;
using HallRental.Services;
using HallRental.Web.Models.ProfileViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HallRental.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IProfileService profileService;
        private readonly IEventsService eventService;

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


        public IActionResult MyReservations()
        {

            string currentUserId =  this.userManager.GetUserId(User);

            var myEvents = this.profileService.MyEvents(currentUserId);

            var myEventsVM = myEvents.Select(e => new MyEventsViewModel
            {
                 Date = e.Date,
                 NumberOfPeople =e.NumberOfPeople,
                 RentTimeDisplay = this.eventService.GetRentTimeDisplay(e.RentTime),
                 HallName = e.HallName,
                 IsConfirmed = e.IsReservationConfirmed,
                 Totalprice = e.Totalprice
                 
            })
            .ToList();

            return View(myEventsVM);
        }
    }
}