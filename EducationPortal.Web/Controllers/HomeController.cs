using Microsoft.AspNetCore.Mvc;
using EducationPortal.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;

namespace EducationPortal.Web.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
    public HomeController()
    { }

    public IActionResult Index()
    {
        HomeViewModel model = new HomeViewModel
        {
            Header = "Education Portal",
            SubHeader = "Your gateway to knowledge and learning",
            WelcomeText = "Improve Your skills by completing various courses or create courses to teach your friends",
            AdviceHeader = "Create Your own course",
            AdviceText = "Label the skills it touches upon and provide the materials"

        };
        return View(model);
    }

    public IActionResult Error()
    {
        // var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

        var model = new ErrorViewModel
        {
            // Message = feature?.Error?.Message ?? "An unknown error occurred."
            Message = "An unknown error occurred."
        };

        TempData.CreateFlash(model.Message, "error");
        return View();
    }
}
