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

    public IActionResult Flash([FromForm] HomeViewModel model)
    {
        TempData.Put("flash", new FlashViewModel()
        {
            Message = model.InputText,
            Type = "info"
        });

        model.Header = "Welcome to the Education Portal";
        model.WelcomeText = "Your gateway to knowledge and learning.";
        return View("Index", model);
    }
}
