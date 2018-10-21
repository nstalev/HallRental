
namespace HallRental.Web.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using HallRental.Web.Models;
    using HallRental.Services.Admin;
    using HallRental.Web.Models.HomeViewModels;
    using Microsoft.AspNetCore.Identity;
    using HallRental.Data.Models;
    using HallRental.Web.Infrastructure;
    using HallRental.Services;

    public class HomeController : Controller
    {
        private readonly IEventsAdminService eventAdminService;
        private readonly UserManager<User> userManager;
        private readonly IHomeService homeService;

        public HomeController(IEventsAdminService eventAdminService,
                               UserManager<User> userManager,
                               IHomeService homeService)
        {
            this.eventAdminService = eventAdminService;
            this.userManager = userManager;
            this.homeService = homeService;
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

        [HttpPost]
        public IActionResult Contact(ContactFormModel contactForm)
        {

            if (!ModelState.IsValid)
            {
                return View(contactForm);
            }

            this.homeService.SendEmail(contactForm.Name, contactForm.Email, contactForm.Subject, contactForm.Message);
            TempData.AddSuccessMessage("Your message has been successfully sent");

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Gallery()
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
