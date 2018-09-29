

namespace HallRental.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using HallRental.Web.Infrastructure;
    using HallRental.Web.Areas.Admin.Models.IdentityViewModels;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using HallRental.Data.Models;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using HallRental.Services.Admin;
    using System;

    [Area("Admin")]
    [Authorize(Roles = GlobalConstants.AdminRole)]
    public class IdentityController : Controller
    {

        private readonly IIdentityService identityService;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private const int pageSize = GlobalConstants.UsersListMaxPageSize;


        public IdentityController(IIdentityService identityService,
                                    UserManager<User> userManager,
                                    RoleManager<IdentityRole> roleManager)
        {
            this.identityService = identityService;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult All(string search, int page = 1)
        {
            if (string.IsNullOrEmpty(search))
            {
                search = "";
            }

            if (page <= 0)
            {
                page = 1;
            }

            int allUsersCount = this.identityService.AllUsersCount(search);

            var users = this.identityService.GetUsers(search, page, pageSize);

            AllUsersViewModel vm = new AllUsersViewModel
            {
                AllUsers = users,
                Search = search,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(allUsersCount / (double)pageSize)
            };

            return View(vm);
        }

        public async Task<IActionResult> Roles(string id)
        {

            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await this.userManager.GetRolesAsync(user);

            var userWithRolew = new UserWithRolesViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Roles = userRoles
            };

            return View(userWithRolew);
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
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
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


        public async Task<IActionResult> Delete(string id)
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

            this.identityService.DeleteEvents(user.Id);

            await this.userManager.DeleteAsync(user);

            return RedirectToAction(nameof(All));

        }


        public IActionResult AddRole(string id)
        {

            var rowSelectListItem = this.roleManager.Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToList();

            return View(rowSelectListItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(string id, string role)
        {
            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var roleExists = await this.roleManager.RoleExistsAsync(role);

            if (!roleExists)
            {
                return NotFound();
            }

            await this.userManager.AddToRoleAsync(user, role);

            return RedirectToAction(nameof(Roles), new { id });
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