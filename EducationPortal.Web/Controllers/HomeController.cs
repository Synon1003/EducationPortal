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
            Header = "HomeHeader",
            SubHeader = "HomeSubHeader",
            WelcomeText = "HomeWelcomeText",
            AdviceHeader = "HomeAdviceHeader",
            AdviceText = "HomeAdviceText"
        };

        return View(model);
    }

    public IActionResult Error()
    {
        var ex = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        string message = ex?.Message ?? "UnexpectedError";

        if (ex is SqlException)
        {
            message = "SqlError";
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

    public IActionResult Forbidden()
    {
        var model = new ErrorViewModel
        {
            ErrorMessage = "ForbiddenError",
            ErrorType = "Forbidden"
        };

        return View("Error", model);
    }
}
