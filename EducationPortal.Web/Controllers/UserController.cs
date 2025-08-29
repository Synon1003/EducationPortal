using Microsoft.AspNetCore.Mvc;
using EducationPortal.Web.Models;
using EducationPortal.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using EducationPortal.Application.Services.Interfaces;
using EducationPortal.Application.Dtos;

namespace EducationPortal.Web.Controllers;

public class UserController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICourseService _courseService;

    public UserController(
        UserManager<ApplicationUser> userManager,
        ICourseService courseService
    )
    {
        _userManager = userManager;
        _courseService = courseService;
    }

    [HttpGet]
    public async Task<IActionResult> EnrollOnCourse(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        await _courseService.EnrollUserOnCourseAsync(user.Id, id);

        return RedirectToAction(nameof(CourseController.Details), "Course", new { id = id });

    }


}
