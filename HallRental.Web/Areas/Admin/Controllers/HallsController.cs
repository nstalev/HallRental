
namespace HallRental.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using HallRental.Services.Admin;
    using HallRental.Web.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;


    [Area("Admin")]
    [Authorize(Roles = GlobalConstants.AdminRole)]
    public class HallsController : Controller
    {

        private readonly IHallsAdminService hallsAdminService;

        public HallsController(IHallsAdminService hallsAdminService)
        {
            this.hallsAdminService = hallsAdminService;
        }

        public IActionResult Index()
        {
            var allActiveHallS = this.hallsAdminService.AllActiveHalls();

            return View(allActiveHallS);
        }


        public IActionResult Details(int id)
        {
            var currentHall = this.hallsAdminService.GetHallById(id);

            if (currentHall == null)
            {
                return NotFound();
            }

            return View(currentHall);
        }
    }
}