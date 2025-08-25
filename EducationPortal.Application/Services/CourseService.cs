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
    private readonly IMaterialRepository _materialRepository;
    private readonly IMapper _mapper;

    public CourseService(
        ICourseRepository courseRepository,
        ISkillRepository skillRepository,
        IMaterialRepository materialRepository,
        IMapper mapper)
    {
        _courseRepository = courseRepository;
        _skillRepository = skillRepository;
        _materialRepository = materialRepository;
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
        Course course = new Course
        {
            Name = courseCreateDto.Name,
            Description = courseCreateDto.Description
        };

        await _courseRepository.InsertAsync(course);
        await InsertCourseSkills(course, courseCreateDto);
        await InsertCourseMaterials(course, courseCreateDto);

        return _mapper.Map<CourseDetailDto>(course);
    }

    private async Task InsertCourseSkills(Course course, CourseCreateDto courseCreateDto)
    {
        foreach (var skill in courseCreateDto.Skills)
        {
            if (_skillRepository.Exists(s => s.Name == skill.Name))
                continue;

            course.Skills.Add(new Skill { Name = skill.Name, Courses = [course] });
        }
        await _skillRepository.InsertRangeAsync(course.Skills.ToList());
        await _courseRepository.UpdateAsync(course);
    }

    private async Task InsertCourseMaterials(Course course, CourseCreateDto courseCreateDto)
    {
        List<Material> materials = [];
        AttachNewVideosToMaterials(course, materials, courseCreateDto);
        AttachNewPublicationsToMaterials(course, materials, courseCreateDto);
        AttachNewArticlesToMaterials(course, materials, courseCreateDto);

        await _materialRepository.InsertRangeAsync(materials);
        await _courseRepository.UpdateAsync(course);
    }

    private void AttachNewVideosToMaterials(
        Course course, List<Material> materials, CourseCreateDto courseCreateDto)
    {
        foreach (var video in courseCreateDto.Videos)
        {
            if (_materialRepository.Exists(
                    m => m.Title == video.Title && m.Type == "Video"))
                continue;

            materials.Add(new Video
            {
                Title = video.Title,
                Duration = video.Duration,
                Quality = video.Quality,
                Courses = [course]
            });
        }
    }

    private void AttachNewPublicationsToMaterials(
        Course course, List<Material> materials, CourseCreateDto courseCreateDto)
    {
        foreach (var publication in courseCreateDto.Publications)
        {
            if (_materialRepository.Exists(
                m => m.Title == publication.Title && m.Type == "Publication"))
                continue;

            materials.Add(new Publication
            {
                Title = publication.Title,
                Authors = publication.Authors,
                Format = publication.Format,
                Pages = publication.Pages,
                PublicationYear = publication.PublicationYear,
                Courses = [course]
            });
        }
    }

    private void AttachNewArticlesToMaterials(
        Course course, List<Material> materials, CourseCreateDto courseCreateDto)
    {
        foreach (var article in courseCreateDto.Articles)
        {
            if (_materialRepository.Exists(
                m => m.Title == article.Title && m.Type == "Article"))
                continue;

            materials.Add(new Article
            {
                Title = article.Title,
                PublicationDate = article.PublicationDate,
                ResourceLink = article.ResourceLink,
                Courses = [course]
            });
        }
    }
}
