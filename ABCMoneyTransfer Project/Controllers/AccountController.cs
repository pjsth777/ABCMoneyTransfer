using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ABCMoneyTransfer_Project.Models;
using ABCMoneyTransfer_Project.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ABCMoneyTransfer_Project.Controllers
{
    public class AccountController : Controller
    {

        // ----------------------------------------------------------------------------------------------------------------------------------

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        // ----------------------------------------------------------------------------------------------------------------------------------

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModels model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }


        // ----------------------------------------------------------------------------------------------------------------------------------

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        // ----------------------------------------------------------------------------------------------------------------------------------

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Your account has been locked out. Please try again later.");
            }
            else if (result.RequiresTwoFactor)
            {
                return RedirectToAction("SendTwoFactorCode");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login Attempt. Please check your credentials.");
            }

            return View(model);
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // ----------------------------------------------------------------------------------------------------------------------------------
    }
}
