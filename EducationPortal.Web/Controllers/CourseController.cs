using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using EducationPortal.Web.Models;
using Microsoft.AspNetCore.Authorization;
using EducationPortal.Application.Services.Interfaces;

namespace EducationPortal.Web.Controllers;

[Authorize]
public class CourseController : Controller
{
    private readonly ICourseService _courseService;
    private readonly IMaterialService _materialService;
    private readonly IMapper _mapper;

    public CourseController(
        ICourseService courseService,
        IMaterialService materialService,
        IMapper mapper)
    {
        _courseService = courseService;
        _materialService = materialService;
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

    public async Task<ActionResult<CourseDetailViewModel>> Details(int id)
    {
        var course = await _courseService.GetCourseWithSkillsAndMaterialsByIdAsync(id);
        return View(_mapper.Map<CourseDetailViewModel>(course));
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
