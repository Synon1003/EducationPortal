using AutoMapper;
using EducationPortal.Data.Entities;
using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Application.Dtos;
using EducationPortal.Application.Services.Interfaces;
using EducationPortal.Application.Exceptions;

namespace EducationPortal.Application.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;

    public CourseService(ICourseRepository courseRepository, IMapper mapper)
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<CourseListDto>> GetAllCoursesWithSkillsAsync()
    {
        var courses = await _courseRepository.GetAllCoursesWithSkillsAsync();

        return _mapper.Map<List<CourseListDto>>(courses);
    }

    public async Task<ICollection<CourseListDto>> GetCoursesByMaterialIdAsync(int materialId)
    {
        var courses = await _courseRepository.GetCoursesByMaterialIdAsync(materialId);

        return _mapper.Map<List<CourseListDto>>(courses);
    }

    public async Task<CourseDetailDto> GetCourseWithSkillsAndMaterialsByIdAsync(int id)
    {
        var course = await _courseRepository.GetCourseWithSkillsAndMaterialsByIdAsync(id);
        if (course == null)
            throw new NotFoundException(nameof(Course), id);

        return _mapper.Map<CourseDetailDto>(course);
    }
}
