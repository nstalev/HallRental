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

        public ProfileController(UserManager<User> userManager,
                                IProfileService profileService)
        {
            this.userManager = userManager;
            this.profileService = profileService;
        }



        public IActionResult Index()
        {
            return View();
        }


        public IActionResult MyReservations()
        {

            string currentUserId =  this.userManager.GetUserId(User);

            var myEvents = this.profileService.MyEvents(currentUserId);

            return View();
        }
    }
}