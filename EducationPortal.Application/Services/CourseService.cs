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

        List<Material> materials = [];
        AttachNewVideosToMaterials(materials, courseCreateDto);
        AttachNewPublicationsToMaterials(materials, courseCreateDto);
        AttachNewArticlesToMaterials(materials, courseCreateDto);

        await InsertCourseMaterials(course, materials);

        return _mapper.Map<CourseDetailDto>(course);
    }

    private async Task InsertCourseSkills(Course course, CourseCreateDto courseCreateDto)
    {
        var skills = courseCreateDto.Skills.Select(s => new Skill { Name = s.Name }).ToList();

        foreach (var skill in skills)
        {
            if (_skillRepository.Exists(s => s.Name == skill.Name))
                continue;
            await _skillRepository.InsertAsync(skill);
        }

        course.CourseSkills = skills.Select(skill => new CourseSkill
        {
            CourseId = course.Id,
            SkillId = skill.Id
        }).ToList();

        await _courseRepository.UpdateAsync(course);
    }

    private void AttachNewVideosToMaterials(List<Material> materials, CourseCreateDto courseCreateDto)
    {
        foreach (var videoDto in courseCreateDto.Videos)
        {
            if (_materialRepository.Exists(m => m.Title == videoDto.Title && m.Type == "Video"))
                continue;

            Video video = new Video
            {
                Title = videoDto.Title,
                Duration = videoDto.Duration,
                Quality = videoDto.Quality
            };

            materials.Add(video);
        }
    }

    private void AttachNewPublicationsToMaterials(List<Material> materials, CourseCreateDto courseCreateDto)
    {
        foreach (var publicationDto in courseCreateDto.Publications)
        {
            if (_materialRepository.Exists(m => m.Title == publicationDto.Title && m.Type == "Publication"))
                continue;

            Publication publication = new Publication
            {
                Title = publicationDto.Title,
                Authors = publicationDto.Authors,
                Format = publicationDto.Format,
                Pages = publicationDto.Pages,
                PublicationYear = publicationDto.PublicationYear
            };

            materials.Add(publication);
        }
    }

    private void AttachNewArticlesToMaterials(List<Material> materials, CourseCreateDto courseCreateDto)
    {
        foreach (var articleDto in courseCreateDto.Articles)
        {
            if (_materialRepository.Exists(m => m.Title == articleDto.Title && m.Type == "Article"))
                continue;

            Article article = new Article
            {
                Title = articleDto.Title,
                PublicationDate = articleDto.PublicationDate,
                ResourceLink = articleDto.ResourceLink
            };

            materials.Add(article);
        }
    }

    private async Task InsertCourseMaterials(Course course, List<Material> materials)
    {
        await _materialRepository.InsertRangeAsync(materials);

        course.CourseMaterials = materials.Select(material => new CourseMaterial
        {
            CourseId = course.Id,
            MaterialId = material.Id
        }).ToList();

        await _courseRepository.UpdateAsync(course);
    }
}
