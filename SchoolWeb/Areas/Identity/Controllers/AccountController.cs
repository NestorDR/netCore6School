using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolWeb.Models;

namespace SchoolWeb.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        // Visit: https://www.c-sharpcorner.com/article/custom-login-register-with-identity-in-asp-net-core-3-1/

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
                    return RedirectToAction(userModel.ReturnUrl);
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

        [HttpGet, AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        // POST api/Account/Register
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel userModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userModel.Email);
                if (user is { EmailConfirmed: false })
                {
                    ModelState.AddModelError("message", "Email not confirmed yet");
                    return View(userModel);

                }
                if (await _userManager.CheckPasswordAsync(user, userModel.Password) == false)
                {
                    ModelState.AddModelError("message", "Invalid credentials");
                    return View(userModel);

                }

                var result = await _signInManager.PasswordSignInAsync(userModel.Email, userModel.Password, userModel.RememberMe, true);

                if (result.Succeeded)
                {
                    await _userManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                else if (result.IsLockedOut)
                {
                    // TODO: return View("AccountLocked");
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid login attempt");
                    return View(userModel);
                }
            }
            return View(userModel);
        }
    }
    }
