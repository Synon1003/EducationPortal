using Microsoft.AspNetCore.Mvc;
using EducationPortal.Web.Models;
using EducationPortal.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using EducationPortal.Application.Services.Interfaces;
using EducationPortal.Application.Dtos;

namespace EducationPortal.Web.Controllers;

[Authorize]

public class UserController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMaterialService _materialService;
    private readonly ICourseService _courseService;

    public UserController(
        UserManager<ApplicationUser> userManager,
        IMaterialService materialService,
        ICourseService courseService
    )
    {
        _userManager = userManager;
        _materialService = materialService;
        _courseService = courseService;
    }

    [HttpGet]
    public async Task<IActionResult> EnrollOnCourse(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        await _courseService.EnrollUserOnCourseAsync(user.Id, id);

        TempData.CreateFlash("You have Enrolled successfully.", "info");

        return RedirectToAction(nameof(CourseController.Details), "Course", new { id = id });
    }

    public async Task<IActionResult> SetCourseMaterialDone(int courseId, int materialId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var isCourseDone = await _courseService.MarkMaterialDone(user.Id, materialId, courseId);

        TempData.CreateFlash(isCourseDone ? "Congratulations! You have finished the course successfully." : "Material has been Marked Done", "info");

        return RedirectToAction(nameof(CourseController.Materials), "Course", new { id = courseId });
    }
}
