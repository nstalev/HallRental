using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HallRental.Web.Models;
using HallRental.Services.Admin;
using HallRental.Web.Models.HomeViewModels;
using Microsoft.AspNetCore.Identity;
using HallRental.Data.Models;
using HallRental.Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace HallRental.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventsAdminService eventAdminService;
        private readonly UserManager<User> userManager;

        public HomeController(IEventsAdminService eventAdminService,
                               UserManager<User> userManager)
        {
            this.eventAdminService = eventAdminService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            HomeIndexVM vm = new HomeIndexVM();

            bool isAdminAuthenticated = User.IsInRole(GlobalConstants.AdminRole);

            if (isAdminAuthenticated)
            {
                int allEventRequestsCount = this.eventAdminService.AllEventRequestsCount();
                vm.AllEventRequestsCount = allEventRequestsCount;

            }


            return View(vm);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
