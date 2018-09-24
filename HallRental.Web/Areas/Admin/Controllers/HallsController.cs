
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
    using AutoMapper.QueryableExtensions;
    using HallRental.Services.Admin.Models.Halls;

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
            var currentHall = this.hallsAdminService.ById(id);

            if (currentHall == null)
            {
                return NotFound();
            }

            return View(currentHall);
        }


        public IActionResult Edit(int id)
        {
            HallsFormServiceModel currentHall = this.hallsAdminService.GetFormModelById(id);

            if (currentHall == null)
            {
                return NotFound();
            }

            return View(currentHall);
        }

        [HttpPost]
        public IActionResult Edit(int id, HallsFormServiceModel hallsFormModel)
        {
            bool hallExists = this.hallsAdminService.Exists(id);

            if (!hallExists)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(hallsFormModel);
            }

            this.hallsAdminService.Edit(id, hallsFormModel.Name,
                        hallsFormModel.HallCapacity,
                        hallsFormModel.MondayFriday8amTo3pm,
                        hallsFormModel.MondayThursday4pmToMN,
                        hallsFormModel.Friday4pmToMN,
                        hallsFormModel.Saturday8amTo3pm,
                        hallsFormModel.Saturday4pmToMN,
                        hallsFormModel.Sunday8amTo3pm,
                        hallsFormModel.Sunday4pmToMN);

            return RedirectToAction(nameof(Index));
        }
    }
}