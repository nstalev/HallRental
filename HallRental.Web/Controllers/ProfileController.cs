﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HallRental.Data.Models;
using HallRental.Services;
using HallRental.Web.Infrastructure;
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

            var myEventsList = this.profileService.MyEvents(currentUserId, page, pageSize);

            int myEventsCount = this.profileService.Total(currentUserId);

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

            return View(vm);
        }


        public IActionResult EventDetails(int id)
        {

            return View();
        }
    }
}