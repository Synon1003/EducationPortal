using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EducationPortal.Web.Models;

namespace EducationPortal.Web.Controllers;

public class HomeController : Controller
{
    public HomeController()
    { }

    public IActionResult Index()
    {
        HomeViewModel model = new HomeViewModel
        {
            Header = "Welcome to the Education Portal",
            WelcomeText = "Your gateway to knowledge and learning."
        };
        return View(model);
    }
}
