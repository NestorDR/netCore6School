using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolWeb.Models;

namespace SchoolWeb.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {

            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        // POST api/Account/Register
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(RegisterModel userModel)
        {
            if (!ModelState.IsValid) return View(userModel);

            var userCheck = await _userManager.FindByEmailAsync(userModel.Email);
            if (userCheck == null)
            {
                var user = new IdentityUser
                {
                    UserName = userModel.Email,
                    NormalizedUserName = userModel.Email,
                    Email = userModel.Email,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                };
                var result = await _userManager.CreateAsync(user, userModel.Password);
                if (result.Succeeded)
                {
                    // return RedirectToAction("Login");
                    return RedirectToAction("/Home");
                }
                else
                {
                    if (result.Errors.Any())
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                    return View(userModel);
                }
            }
            else
            {
                ModelState.AddModelError("", "Email already exists.");
                return View(userModel);
            }
            
        }
    }
}
