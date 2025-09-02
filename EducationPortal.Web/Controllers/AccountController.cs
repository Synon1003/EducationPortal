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

    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
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
            TempData.CreateFlash("Registration failed.", "error");

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
            TempData.CreateFlash("Account created successfully. You are now logged in.", "info");

            return RedirectToAction("List", "Course");
        }

        foreach (IdentityError error in result.Errors)
            ModelState.AddModelError("Register", error.Description);
        TempData.CreateFlash("Registration failed.", "error");

        return View(registerViewModel);
    }

    [NonAction]
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
            TempData.CreateFlash("Login failed.", "error");

            return View(loginViewModel);
        }

        var result = await _signInManager.PasswordSignInAsync(
            loginViewModel.Email, loginViewModel.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            TempData.CreateFlash("Logged in successfully.", "info");

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return LocalRedirect(returnUrl);

            return RedirectToAction("List", "Course");
        }

        ModelState.AddModelError("Login", "Invalid email or password.");
        TempData.CreateFlash("Login failed.", "error");

        return View(loginViewModel);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        TempData.CreateFlash("Logged out successfully.", "info");

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
            return Problem("Invalid theme");
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
}
