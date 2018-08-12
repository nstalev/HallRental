
namespace HallRental.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using HallRental.Web.Infrastructure;
    using HallRental.Services;

    [Authorize(Roles = GlobalConstants.AdminRole)]
    public class IdentityController : Controller
    {

        private readonly IIdentityService identityService;

        public IdentityController(IIdentityService identityService)
        {
            this.identityService = identityService;
        }


        public IActionResult All()
        {
            var users = this.identityService.AllUsers();

            return View(users);
        }
    }
}