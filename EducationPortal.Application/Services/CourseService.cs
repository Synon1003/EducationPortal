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
    private readonly ISkillRepository _skillRepository;
    private readonly IMapper _mapper;

    public CourseService(
        ICourseRepository courseRepository,
        ISkillRepository skillRepository,
        IMapper mapper)
    {
        _courseRepository = courseRepository;
        _skillRepository = skillRepository;
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

    public async Task<CourseListDto> GetCourseByIdAsync(int id)
    {
        var course = await _courseRepository.GetByIdAsync(id);
        if (course == null)
            throw new NotFoundException(nameof(Course), id);

        return _mapper.Map<CourseListDto>(course);
    }

    public async Task<CourseDetailDto> GetCourseWithSkillsAndMaterialsByIdAsync(int id)
    {
        var course = await _courseRepository.GetCourseWithSkillsAndMaterialsByIdAsync(id);
        if (course == null)
            throw new NotFoundException(nameof(Course), id);

        return _mapper.Map<CourseDetailDto>(course);
    }

    public async Task<CourseDetailDto> CreateCourseAsync(CourseCreateDto courseCreateDto)
    {
        Course course = _mapper.Map<Course>(courseCreateDto);

        var skills = courseCreateDto.Skills.Select(s => new Skill { Name = s.Name }).ToList();

        await _courseRepository.InsertAsync(course);

        // TODO check for duplicates)
        foreach (var skill in skills)
        {
            await _skillRepository.InsertAsync(skill);
        }

        course.CourseSkills = skills.Select(skill => new CourseSkill
        {
            CourseId = course.Id,
            SkillId = skill.Id
        }).ToList();

        await _courseRepository.UpdateAsync(course);

        return _mapper.Map<CourseDetailDto>(course);
    }
}
