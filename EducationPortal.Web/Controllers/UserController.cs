using Microsoft.AspNetCore.Mvc;
using EducationPortal.Web.Models;
using EducationPortal.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using EducationPortal.Application.Services.Interfaces;
using AutoMapper;

namespace EducationPortal.Web.Controllers;

[Authorize]
public class UserController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICourseService _courseService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(
        UserManager<ApplicationUser> userManager,
        ICourseService courseService,
        IUserService userService,
        IMapper mapper
    )
    {
        _userManager = userManager;
        _userService = userService;
        _courseService = courseService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> EnrollOnCourse(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var isCourseDone = await _courseService.EnrollUserOnCourseAsync(user.Id, id);

        TempData.CreateFlash(isCourseDone ? "Congratulations! You passed successfully." : "You enrolled successfully.", "info");

        return RedirectToAction(nameof(CourseController.Details), "Course", new { id = id });
    }

    [HttpGet]
    public async Task<IActionResult> LeaveCourse(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        await _courseService.LeaveCourseAsync(user.Id, id);

        TempData.CreateFlash("You left the course successfully.", "info");

        return RedirectToAction(nameof(CourseController.Details), "Course", new { id = id });
    }

    [HttpGet]
    public async Task<IActionResult> SetCourseMaterialDone(int courseId, int materialId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var isCourseDone = await _courseService.MarkMaterialDone(user.Id, materialId, courseId);

        TempData.CreateFlash(isCourseDone ? "Congratulations! You passed successfully." : "Material marked as Done.", "info");

        return RedirectToAction(nameof(CourseController.Materials), "Course", new { id = courseId });
    }

    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();


        var videos = _mapper.Map<List<VideoViewModel>>(
            await _userService.GetAcquiredVideosByUserIdAsync(user.Id));
        var publications = _mapper.Map<List<PublicationViewModel>>(
            await _userService.GetAcquiredPublicationsByUserIdAsync(user.Id));
        var articles = _mapper.Map<List<ArticleViewModel>>(
            await _userService.GetAcquiredArticlesByUserIdAsync(user.Id));

        var userSkills = _mapper.Map<List<UserSkillViewModel>>(
            await _userService.GetAcquiredSkillsByUserIdAsync(user.Id)
        );

        UserProfileViewModel profile = new UserProfileViewModel()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            UserName = user.UserName!,
            Videos = videos,
            Publications = publications,
            Articles = articles,
            UserSkills = userSkills
        };

        return View(profile);
    }
}
