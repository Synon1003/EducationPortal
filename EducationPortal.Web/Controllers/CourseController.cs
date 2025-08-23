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
    private readonly IMapper _mapper;

    public CourseController(ICourseService courseService, IMapper mapper)
    {
        _courseService = courseService;
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
}
