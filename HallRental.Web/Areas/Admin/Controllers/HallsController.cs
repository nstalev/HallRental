
namespace HallRental.Web.Areas.Admin.Controllers
{
    using HallRental.Services.Admin;
    using HallRental.Web.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
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


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HallsFormServiceModel hallsFormModel)
        {
            if (!ModelState.IsValid)
            {
                return View(hallsFormModel);
            }

            this.hallsAdminService.Create(hallsFormModel.Name,
                        hallsFormModel.HallCapacity,
                        hallsFormModel.MondayFriday8amTo3pm,
                        hallsFormModel.MondayThursday4pmToMN,
                        hallsFormModel.Friday4pmToMN,
                        hallsFormModel.Saturday8amTo3pm,
                        hallsFormModel.Saturday4pmToMN,
                        hallsFormModel.Sunday8amTo3pm,
                        hallsFormModel.Sunday4pmToMN,
                        hallsFormModel.TablesAndChairsCostPerPerson,
                        hallsFormModel.SecurityGuardCostPerHour);


            TempData.AddSuccessMessage($"Hall {hallsFormModel.Name} has been created");

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Details(int id)
        {
            var currentHall = this.hallsAdminService.ById(id);

            if (currentHall == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(currentHall);
        }


        public IActionResult Edit(int id)
        {
            bool hallExists = this.hallsAdminService.Exists(id);

            if (!hallExists)
            {
                return RedirectToAction(nameof(Index));
            }

            HallsFormServiceModel currentHall = this.hallsAdminService.GetFormModelById(id);

            if (currentHall == null)
            {
                return NotFound();
            }

            return View(currentHall);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, HallsFormServiceModel hallsFormModel)
        {
            bool hallExists = this.hallsAdminService.Exists(id);

            if (!hallExists)
            {
                return RedirectToAction(nameof(Index));
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
                        hallsFormModel.Sunday4pmToMN,
                        hallsFormModel.TablesAndChairsCostPerPerson,
                        hallsFormModel.SecurityGuardCostPerHour);

            TempData.AddSuccessMessage($"Hall {hallsFormModel.Name} has been edited");

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Disable (int id)
        {
            bool hallExists = this.hallsAdminService.Exists(id);

            if (!hallExists)
            {
                return RedirectToAction(nameof(Index));
            }

            this.hallsAdminService.DisableHall(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Enable(int id)
        {
            bool hallExists = this.hallsAdminService.Exists(id);

            if (!hallExists)
            {
                return RedirectToAction(nameof(Index));
            }

            this.hallsAdminService.EnableHall(id);

            return RedirectToAction(nameof(Index));
        }


        public IActionResult DisabledHalls()
        {

            var disabledHalls = this.hallsAdminService.AllDisabledHalls();

            return View(disabledHalls);
        }
    }
}