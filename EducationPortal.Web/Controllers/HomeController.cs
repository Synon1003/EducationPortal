using Microsoft.AspNetCore.Mvc;
using EducationPortal.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using EducationPortal.Application.Exceptions;
using Microsoft.Data.SqlClient;

namespace EducationPortal.Web.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
    [HttpGet]
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
        var ex = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        string message = ex?.Message ?? "An unexpected error occurred. Please try again later.";

        if (ex is SqlException)
        {
            message = "We are having trouble connecting to the database. Please try again later.";
        }
        else if (ex is ValidationException vex)
        {
            message = string.Join("\n", vex.Errors);
        }

        var model = new ErrorViewModel
        {
            ErrorMessage = message,
            ErrorType = ex?.GetType().Name ?? "Exception"
        };

        return View(model);
    }
}
