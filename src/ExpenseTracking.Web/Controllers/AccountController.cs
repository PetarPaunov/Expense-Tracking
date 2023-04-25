namespace ExpenseTracking.Web.Controllers
{
    using ExpenseTracking.Core.Models.AccountViewModels;
    using ExpenseTracking.Infrastructure.Models.Account;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static ExpenseTracking.Web.Constants.RedirectConstants;
    using static ExpenseTracking.Core.Constants.ErrorConstants;
    using static ExpenseTracking.Core.Constants.RoleConstants;
    using ExpenseTracking.Infrastructure.ExpenseTables.Wallet;

    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, 
                                 SignInManager<ApplicationUser> signInManager,
                                 RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.Identity!.IsAuthenticated)
            {
                // Have to think about it !!
                return RedirectToAction("Index", "UserHome", new { area = "User" });
            }

            var model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser()
            {
                UserName = model.UserName,
                Wallet = new Wallet(),
            };

            var result = await this.userManager
                .CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var roleExists = await this.roleManager.RoleExistsAsync(UserRole);

                if (!roleExists)
                {
                    await this.roleManager.CreateAsync(new IdentityRole(UserRole));
                }

                await this.userManager.AddToRoleAsync(user, UserRole);

                return RedirectToAction(nameof(Login));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(String.Empty, error.Description);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                // Have to think about it !!
                return RedirectToAction("Index", "UserHome", new { area = "User" });
            }

            var model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await this.userManager
                .FindByNameAsync(model.UserName);

            if (user != null)
            {
                var result = await this.signInManager
                    .PasswordSignInAsync(user, model.Password, model.IsPersistent, false);

                if (result.Succeeded)
                {
                    // Have to think about it !!
                    return RedirectToAction("Index", "UserHome", new { area = "User"});
                }
            }

            ModelState.AddModelError(String.Empty, SomethingWentWrong);

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction(Index, Home, new {area = "default"});
        }
    }
}