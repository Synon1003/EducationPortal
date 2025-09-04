using Microsoft.AspNetCore.Mvc;
using EducationPortal.Web.Models;
using EducationPortal.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using EducationPortal.Web.Filters;

namespace EducationPortal.Web.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<AccountController> _logger;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ILogger<AccountController> logger
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Register()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
            return RedirectToAction("List", "Course");

        return View(new RegisterViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromForm] RegisterViewModel registerViewModel)
    {
        if (!ModelState.IsValid)
        {
            TempData.Put<List<string>>("errors",
                [.. ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)]);
            TempData.CreateFlash("RegistrationFailedFlash", "error");

            return View(registerViewModel);
        }

        ApplicationUser user = new ApplicationUser()
        {
            FirstName = registerViewModel.FirstName,
            LastName = registerViewModel.LastName,
            Email = registerViewModel.Email,
            UserName = registerViewModel.Email,
        };

        IdentityResult result = await _userManager.CreateAsync(user, registerViewModel.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            TempData.CreateFlash("AccountCreatedSuccessfullyYouAreNowLoggedInFlash.", "info");
            _logger.LogInformation("Created <User Email={Email}>", user.Email);

            return RedirectToAction("List", "Course");
        }

        foreach (IdentityError error in result.Errors)
            ModelState.AddModelError("Register", error.Description);
        TempData.CreateFlash("RegistrationFailedFlash", "error");

        return View(registerViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(email);
        return Json(user == null);
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
            return RedirectToAction("List", "Course");

        return View(new LoginViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromForm] LoginViewModel loginViewModel, string? returnUrl)
    {
        if (!ModelState.IsValid)
        {
            TempData.Put<List<string>>("errors",
                [.. ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)]);
            TempData.CreateFlash("LogInFailedFlash", "error");

            return View(loginViewModel);
        }

        var result = await _signInManager.PasswordSignInAsync(
            loginViewModel.Email, loginViewModel.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            TempData.CreateFlash("LoggedInSuccessfullyFlash", "info");
            _logger.LogInformation("Logged in <User Email={Email}>", loginViewModel.Email);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return LocalRedirect(returnUrl);

            return RedirectToAction("List", "Course");
        }

        ModelState.AddModelError("Login", "InvalidEmailOrPasswordFlash");
        TempData.CreateFlash("LogInFailedFlash", "error");

        return View(loginViewModel);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        await _signInManager.SignOutAsync();

        TempData.CreateFlash("LoggedOutSuccessfullyFlash", "info");
        _logger.LogInformation("Logged out <User Email={Email}>", user.Email);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Authorize]
    [FetchOnly]
    public async Task<IActionResult> SetUserTheme(string theme)
    {
        List<string> validThemes = ["business", "corporate"];
        if (!validThemes.Contains(theme))
        {
            return BadRequest("Invalid theme.");
        }

        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            user.Theme = theme;
            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
        }

        return Ok(new { theme });
    }

    [HttpGet]
    [Authorize]
    [FetchOnly]
    public async Task<IActionResult> SetUserLanguage(string language)
    {
        List<string> validLanguages = ["en", "hu"];
        if (!validLanguages.Contains(language))
        {
            return BadRequest("Invalid language.");
        }

        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            user.Language = language;
            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
        }

        return Ok();
    }
}
