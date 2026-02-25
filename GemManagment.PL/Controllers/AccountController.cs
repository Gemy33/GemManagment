using GemManagment.BLL.Services.Interfaces;
using GemManagment.BLL.ViewModels.Account;
using GemManagment.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GemManagment.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccount account;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountController(IAccount account,SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this.account = account;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var user =  await account.ValidatedUser(loginViewModel);
            if (user is not null)
            {

                var addedUser = await signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);
                if (addedUser != null)
                {
                    if (addedUser.IsLockedOut)
                    {
                        ModelState.AddModelError("InvalidLogin", "This User is locked out");
                    }
                    else if (addedUser.IsNotAllowed)
                    {
                        ModelState.AddModelError("InvalidLogin", "This User is not allowed");
                    }
                    else if (addedUser.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("InvalidLogin", "Invalid Login Email Or Password Wrong !");
                return View(loginViewModel);

            }

            ModelState.AddModelError("InvalidLogin", "Invalid Login Attempt");
            return View(loginViewModel);
        }
        public async Task<ActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            
            return RedirectToAction(nameof(Login));
        }

        public ActionResult AccessDenied()
        {
            return View("AccessDeniaed");
        }

    }
}
