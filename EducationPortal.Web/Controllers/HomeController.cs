using Microsoft.AspNetCore.Mvc;
using EducationPortal.Web.Models;
using Microsoft.AspNetCore.Authorization;

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
}
