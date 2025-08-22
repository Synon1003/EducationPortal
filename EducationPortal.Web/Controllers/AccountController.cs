using Microsoft.AspNetCore.Mvc;
using EducationPortal.Web.Models;

namespace EducationPortal.Web.Controllers;

public class AccountController : Controller
{
    public AccountController()
    { }

    [HttpGet]
    public IActionResult Register()
    {
        return View(new RegisterViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromForm] RegisterViewModel registerViewModel)
    {
        if (!ModelState.IsValid)
        {
            TempData.Put<List<string>>("errors",
                [.. ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)]);
            TempData.CreateFlash("Validation failed.", "error");

            return View(registerViewModel);
        }

        ModelState.AddModelError("Register", "Register failed for no reason.");
        TempData.CreateFlash("Registration failed.", "error");

        return View(registerViewModel);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View(new LoginViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromForm] LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid)
        {
            TempData.Put<List<string>>("errors",
                [.. ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)]);
            TempData.CreateFlash("Validation failed.", "error");

            return View(loginViewModel);
        }

        ModelState.AddModelError("Login", "Login failed for no reason.");
        TempData.CreateFlash("Login failed.", "error");

        return View(loginViewModel);
    }
}
