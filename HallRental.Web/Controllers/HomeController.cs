
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
    using HallRental.Web.Services;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly IEventsAdminService eventAdminService;
        private readonly UserManager<User> userManager;
        private readonly IEmailSender emailSender;
        private readonly IEmailService emailService;

        public HomeController(IEventsAdminService eventAdminService,
                               UserManager<User> userManager,
                               IEmailSender emailSender,
                               IEmailService emailService)
        {
            this.eventAdminService = eventAdminService;
            this.userManager = userManager;
            this.emailSender = emailSender;
            this.emailService = emailService;
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
        public async Task<IActionResult> Contact(ContactFormModel contactForm)
        {

            if (!ModelState.IsValid)
            {
                return View(contactForm);
            }


            string messageBody = this.emailService.GetEmailTextBodyFromContactForm(contactForm.Email,
                                                             contactForm.Name,
                                                             contactForm.Message);

            await this.emailSender.SendEmailAsync(GlobalConstants.HomeEmail, contactForm.Subject, messageBody);

            TempData.AddSuccessMessage("Your message has been successfully sent");

            return RedirectToAction(nameof(Contact));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Gallery()
        {
            return View();
        }


        public IActionResult TermsAndConditions()
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
