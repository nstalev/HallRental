
namespace HallRental.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using HallRental.Web.Infrastructure;
    using HallRental.Services;
    using HallRental.Web.Models.IdentityViewModels;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using HallRental.Data.Models;

    [Authorize(Roles = GlobalConstants.AdminRole)]
    public class IdentityController : Controller
    {

        private readonly IIdentityService identityService;
        private readonly UserManager<User> userManager;

        public IdentityController(IIdentityService identityService,
                                    UserManager<User> userManager)
        {
            this.identityService = identityService;
            this.userManager = userManager;
        }


        public IActionResult All()
        {
            var users = this.identityService.AllUsers();

            return View(users);
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(CreateUserViewModel userModel)
        {

            if (!ModelState.IsValid)
            {
                return View(userModel);
            }

            var user = new User()
            {
                UserName = userModel.UserName,
                Email = userModel.Email,
                PhoneNumber = userModel.PhoneNumber
            };

            var result = await this.userManager.CreateAsync(user, userModel.Password);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(All));
            }
            else
            {
                this.AddModelErrors(result);
                return View(userModel);
            }

        }


        public async Task<IActionResult> Delete (string id)
        {

            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userVM = new DeleteUserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName
            };

            return View(userVM);
        }


        public async Task<IActionResult> Destroy(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

             await this.userManager.DeleteAsync(user);

            return RedirectToAction(nameof(All));

        }



        private void AddModelErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}