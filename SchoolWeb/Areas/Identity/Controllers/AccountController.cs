using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EmailService;
using SchoolWeb.Controllers;
using SchoolWeb.Models;

namespace SchoolWeb.Areas.Identity.Controllers;

[Area("Identity")]
public class AccountController : Controller
{
    // Visit: https://www.c-sharpcorner.com/article/custom-login-register-with-identity-in-asp-net-core-3-1/
    //        https://code-maze.com/user-registration-aspnet-core-identity/  

    // 1.Previous to extend IdentityUser class with the User class
    // private readonly UserManager<IdentityUser> _userManager;
    // private readonly SignInManager<IdentityUser> _signInManager;

    // AutoMapper is a simple library that helps us to transform one object type to another.
    // It is a convention-based object-to-object mapper that requires very little configuration. 
    // Visit: https://code-maze.com/automapper-net-core/
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;        // To automate Authentication Process
    private readonly IEmailSender _emailSender;

    // 2.Previous to extend IdentityUser class with the User class
    //public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    public AccountController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender)
    {
        this._mapper = mapper;
        this._userManager = userManager;
        this._signInManager = signInManager;
        this._emailSender = emailSender;
    }

    #region Register
    [HttpGet, AllowAnonymous]
    public IActionResult Register()
    {
        return View();
    }

    // POST api/Account/Register
    [HttpPost, AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterModel userModel)
    {
        if (!ModelState.IsValid) return View(userModel);

        /*
        // User check is not required, it is replaced with options.User.RequireUniqueEmail = true; (in Program.cs)
        var userCheck = await _userManager.FindByEmailAsync(userModel.Email);
        if (userCheck != null)        {
            ModelState.AddModelError("", "Email already exists.");
            return View(userModel);
        }
        */

        // Use AutoMapper library
        var user = _mapper.Map<User>(userModel);
        user.EmailConfirmed = true;     // Checked in Login
        user.PhoneNumberConfirmed = true;

        // Create user action
        var result = await _userManager.CreateAsync(user, userModel.Password);

        // If the action was successful, add standard role and redirect the user to the login page.
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Visitor");
            return RedirectToAction(nameof(AccountController.Login));
        }

        // The action failed, add errors to model state and return view
        foreach (var error in result.Errors)
        {
            ModelState.TryAddModelError("", error.Description);
        }

        return View(userModel);
    }
    #endregion

    #region Login
    [HttpGet, AllowAnonymous]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    // POST api/Account/Register
    [HttpPost, AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel userModel, string? returnUrl = null)
    {
        if (!ModelState.IsValid) return View(userModel);

        var user = await _userManager.FindByEmailAsync(userModel.Email);
        if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
        {
            ModelState.AddModelError("", "Email not confirmed yet");
            return View(userModel);
        }
        if (await _userManager.CheckPasswordAsync(user, userModel.Password) == false)
        {
            ModelState.AddModelError("", "Invalid credentials");
            return View(userModel);
        }

        // Automate Authentication Process using SignInManger<TUser> class, that provides the API for user sign-in with a lot of helper methods.
        var result = await this._signInManager.PasswordSignInAsync(userModel.Email, userModel.Password, userModel.RememberMe, false);
        if (result.Succeeded)
        {
            return RedirectToLocal(returnUrl);
        }
        
        if (result.IsLockedOut)
        {
            // TODO: return View("AccountLocked");
        }
        else
        {
            ModelState.AddModelError("", "Invalid login attempt");
            return View(userModel);
        }

        return View(userModel);
    }
    #endregion

    #region Logout
    public async Task<IActionResult> Logout()
    {
        await this._signInManager.SignOutAsync();
        return RedirectToLocal(null);
    }
    #endregion

    #region Forgot and Reset Password
    [HttpGet, AllowAnonymous]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
    {
        if (!ModelState.IsValid) return View(forgotPasswordModel);

        // Get the user from the database by its email.
        User? user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);

        // If the user does not exist, simply redirect that user to the confirmation page,
        //  without creating a "user does not exist" message (good security practice).
        if (user == null) return RedirectToAction(nameof(ForgotPasswordConfirmation));

        // Generate a token with the GeneratePasswordResetTokenAsync method and create
        //  an callback link to the action to use for the reset logic.
        string? token = await _userManager.GeneratePasswordResetTokenAsync(user);
        string? callback = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);

        // Create and send an email message to the provided email address.
        Tuple<string, string>[] to = { Tuple.Create(user.FullName, user.Email) };
        Message message = new(to, "Reset password token", callback, new FormFileCollection());
        await _emailSender.SendEmailAsync(message);

        // Redirect the user to the ForgotPasswordConfirmation view.
        return RedirectToAction(nameof(ForgotPasswordConfirmation));
    }

    public IActionResult ForgotPasswordConfirmation()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ResetPassword(string token, string email)
    {
        ResetPasswordModel resetPasswordModel = new ResetPasswordModel { Token = token, Email = email };
        return View(resetPasswordModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
    {
        if (!ModelState.IsValid)
            return View(resetPasswordModel);

        // Get the user from the database by its email.
        var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);

        // If the user does not exist, simply redirect that user to the confirmation page,
        //  without creating a "user does not exist" message (good security practice).
        if (user == null) RedirectToAction(nameof(ResetPasswordConfirmation));

        // Password reset action
        var result = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);

        // If the action was successful, redirect the user to the confirmation page.
        if (result.Succeeded) return RedirectToAction(nameof(ResetPasswordConfirmation));

        // The action failed, add errors to model state and return view
        foreach (var error in result.Errors)
        {
            ModelState.TryAddModelError("", error.Description);
        }

        return View();
    }

    [HttpGet]
    public IActionResult ResetPasswordConfirmation()
    {
        return View();
    }
    #endregion

    /// <summary>
    /// Checks if the returnUrl is a local URL and if it is, redirects the user to that address, otherwise
    ///  redirects the user to the Home page.
    /// </summary>
    /// <param name="returnUrl"></param>
    /// <returns></returns>
    private IActionResult RedirectToLocal(string? returnUrl)
    {
        if (Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);

        return RedirectToAction(nameof(HomeController.Index), "Home", new { Area = "" });
    }
}