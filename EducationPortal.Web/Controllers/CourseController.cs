using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using EducationPortal.Web.Models;
using Microsoft.AspNetCore.Authorization;
using EducationPortal.Application.Services.Interfaces;
using EducationPortal.Application.Dtos;
using Microsoft.AspNetCore.Identity;
using EducationPortal.Data.Entities;

namespace EducationPortal.Web.Controllers;

[Authorize]
public class CourseController : Controller
{
    private readonly ICourseService _courseService;
    private readonly IMaterialService _materialService;
    private readonly IUserService _userService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public CourseController(
        ICourseService courseService,
        IMaterialService materialService,
        IUserService userService,
        UserManager<ApplicationUser> userManager,
        IMapper mapper)
    {
        _courseService = courseService;
        _materialService = materialService;
        _userService = userService;
        _userManager = userManager;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> List(string filter)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var courses = await _courseService.GetFilteredCoursesWithSkillsAsync(user.Id, filter);

        var courseListViewModels = _mapper.Map<List<CourseListViewModel>>(courses);

        foreach (var course in courseListViewModels)
            course.UserCourse = _mapper.Map<UserCourseViewModel>
            (await _courseService.GetUserCourseAsync(user.Id, course.Id));

        var listCoursesViewModel = new ListCoursesViewModel
        {
            Courses = courseListViewModels,
            TotalCount = courses.Count,
            CurrentOption = filter
        };

        return View(listCoursesViewModel);
    }

    [HttpGet]
    public async Task<ActionResult<CourseDetailViewModel>> Details(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var course = await _courseService.GetCourseWithRelationshipsByIdAsync(id);

        var courseDetailViewModel = _mapper.Map<CourseDetailViewModel>(course);

        courseDetailViewModel.UserCourse = _mapper.Map<UserCourseViewModel>
            (await _courseService.GetUserCourseAsync(user.Id, course.Id));

        return View(courseDetailViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var createdVideosByUser = await _userService.GetVideosCreatedByUserIdAsync(user.Id);
        var createdPublicationsByUser = await _userService.GetPublicationsCreatedByUserIdAsync(user.Id);
        var createdArticlesByUser = await _userService.GetArticlesCreatedByUserIdAsync(user.Id);

        var courseCreateViewModel = new CourseCreateViewModel()
        {
            PreviouslyCreatedVideos = _mapper.Map<List<VideoViewModel>>(createdVideosByUser),
            PreviouslyCreatedPublications = _mapper.Map<List<PublicationViewModel>>(createdPublicationsByUser),
            PreviouslyCreatedArticles = _mapper.Map<List<ArticleViewModel>>(createdArticlesByUser)
        };

        return View(courseCreateViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CourseCreateViewModel courseCreateViewModel)
    {
        if (!ModelState.IsValid)
        {
            TempData.Put<List<string>>("errors",
                [.. ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)]);
            TempData.CreateFlash("Course creation failed.", "error");

            return View(courseCreateViewModel);
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var courseCreateDto = _mapper.Map<CourseCreateDto>(courseCreateViewModel) with { CreatedBy = user.Id! };

        await _courseService.CreateCourseAsync(courseCreateDto);
        TempData.CreateFlash("Course created successfully.", "info");

        return RedirectToAction(nameof(List));
    }

    [HttpGet]
    public async Task<IActionResult> Materials(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var course = await _courseService.GetCourseByIdAsync(id);

        var materialDtos = await _materialService.
            GetMaterialsByCourseIdAsync(courseId: id);

        (int Total, int Done) counter = (0, 0);
        var materials = _mapper.Map<List<MaterialViewModel>>(materialDtos);
        foreach (var material in materials)
        {
            material.IsDoneByUser = _courseService.IsUserDoneWithMaterial(user.Id, material.Id);
            counter.Total++;
            if (material.IsDoneByUser)
                counter.Done++;
        }

        var listMaterialsViewModel = new ListMaterialViewModel
        {
            CourseId = id,
            CourseName = course.Name,
            Videos = materials.Where(m => m.Type == "Video").ToList(),
            Publications = materials.Where(m => m.Type == "Publication").ToList(),
            Articles = materials.Where(m => m.Type == "Article").ToList(),
            MaterialsTotal = counter.Total,
            MaterialsDone = counter.Done,
        };

        return View(listMaterialsViewModel);
    }
}
