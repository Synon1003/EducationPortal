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
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public CourseController(
        ICourseService courseService,
        IMaterialService materialService,
        UserManager<ApplicationUser> userManager,
        IMapper mapper)
    {
        _courseService = courseService;
        _materialService = materialService;
        _userManager = userManager;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var courses = await _courseService.GetAllCoursesWithSkillsAsync();

        var listCoursesViewModel = new ListCoursesViewModel
        {
            Courses = _mapper.Map<List<CourseListViewModel>>(courses),
            TotalCount = courses.Count
        };

        return View(listCoursesViewModel);
    }

    [HttpGet]
    public async Task<ActionResult<CourseDetailViewModel>> Details(int id)
    {
        var course = await _courseService.GetCourseWithRelationshipsByIdAsync(id);

        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var courseDetailViewModel = _mapper.Map<CourseDetailViewModel>(course);

        courseDetailViewModel.UserCourse = _mapper.Map<UserCourseViewModel>
            (await _courseService.GetUserCourseAsync(user.Id, course.Id));

        return View(courseDetailViewModel);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new CourseCreateViewModel());
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
        var course = await _courseService.GetCourseByIdAsync(id);

        var materials = await _materialService.
            GetMaterialsByCourseIdAsync(courseId: id);

        var videos = materials.Where(m => m.Type == "Video").ToList();
        var publications = materials.Where(m => m.Type == "Publication").ToList();
        var articles = materials.Where(m => m.Type == "Article").ToList();

        var listMaterialsViewModel = new ListMaterialViewModel
        {
            CourseId = id,
            CourseName = course.Name,
            Videos = _mapper.Map<List<MaterialViewModel>>(videos),
            Publications = _mapper.Map<List<MaterialViewModel>>(publications),
            Articles = _mapper.Map<List<MaterialViewModel>>(articles)
        };

        return View(listMaterialsViewModel);
    }
}
