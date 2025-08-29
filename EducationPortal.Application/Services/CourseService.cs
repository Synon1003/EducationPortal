using AutoMapper;
using EducationPortal.Data.Entities;
using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Application.Dtos;
using EducationPortal.Application.Services.Interfaces;
using EducationPortal.Application.Exceptions;

namespace EducationPortal.Application.Services;

public class CourseService : ICourseService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CourseService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ICollection<CourseListDto>> GetAllCoursesWithSkillsAsync()
    {
        var courses = await _unitOfWork.CourseRepository.GetAllCoursesWithSkillsAsync();

        return _mapper.Map<List<CourseListDto>>(courses);
    }

    public async Task<ICollection<CourseListDto>> GetCoursesByMaterialIdAsync(int materialId)
    {
        var courses = await _unitOfWork.CourseRepository.GetCoursesByMaterialIdAsync(materialId);

        return _mapper.Map<List<CourseListDto>>(courses);
    }

    public async Task<CourseListDto> GetCourseByIdAsync(int id)
    {
        var course = await _unitOfWork.CourseRepository.GetByIdAsync(id);
        if (course == null)
            throw new NotFoundException(nameof(Course), id);

        return _mapper.Map<CourseListDto>(course);
    }

    public async Task<CourseDetailDto> GetCourseWithRelationshipsByIdAsync(int id)
    {
        var course = await _unitOfWork.CourseRepository.GetCourseWithRelationshipsByIdAsync(id);
        if (course == null)
            throw new NotFoundException(nameof(Course), id);

        return _mapper.Map<CourseDetailDto>(course);
    }

    public async Task<CourseDetailDto> CreateCourseAsync(CourseCreateDto courseCreateDto)
    {
        if (_unitOfWork.CourseRepository.Exists(c => c.Name == courseCreateDto.Name))
            throw new BadRequestException($"Course ({courseCreateDto.Name}) already exists in the db.");

        Course course = new Course
        {
            Name = courseCreateDto.Name,
            Description = courseCreateDto.Description,
            CreatedBy = courseCreateDto.CreatedBy ?? throw new BadRequestException("User cannot be null.")
        };

        await _unitOfWork.CourseRepository.InsertAsync(course);
        await InsertCourseSkills(course, courseCreateDto);
        await InsertCourseMaterials(course, courseCreateDto);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<CourseDetailDto>(course);
    }

    public bool IsUserEnrolledOnCourse(Guid userId, int courseId)
    {
        return _unitOfWork.UserCourseRepository.Exists(c => c.UserId == userId && c.CourseId == courseId);
    }

    public async Task<UserCourseDto?> GetUserCourseAsync(Guid userId, int courseId)
    {
        var userCourse = await _unitOfWork.UserCourseRepository.GetByFilterAsync(
            c => c.UserId == userId && c.CourseId == courseId);

        return _mapper.Map<UserCourseDto>(userCourse);
    }

    public async Task EnrollUserOnCourseAsync(Guid userId, int courseId)
    {
        await _unitOfWork.UserCourseRepository.InsertAsync(new UserCourse()
        {
            UserId = userId,
            CourseId = courseId
        });
        await _unitOfWork.SaveChangesAsync();
    }

    private async Task InsertCourseSkills(Course course, CourseCreateDto courseCreateDto)
    {
        foreach (var skill in courseCreateDto.Skills)
        {
            if (_unitOfWork.SkillRepository.Exists(s => s.Name == skill.Name))
                throw new BadRequestException($"Skill ({skill.Name}) already exists in the db.");

            course.Skills.Add(new Skill { Name = skill.Name, Courses = [course] });
        }
        await _unitOfWork.SkillRepository.InsertRangeAsync(course.Skills.ToList());
    }

    private async Task InsertCourseMaterials(Course course, CourseCreateDto courseCreateDto)
    {
        List<Material> materials = [];
        AttachNewVideosToMaterials(course, materials, courseCreateDto);
        AttachNewPublicationsToMaterials(course, materials, courseCreateDto);
        AttachNewArticlesToMaterials(course, materials, courseCreateDto);

        await _unitOfWork.MaterialRepository.InsertRangeAsync(materials);
    }

    private void AttachNewVideosToMaterials(
        Course course, List<Material> materials, CourseCreateDto courseCreateDto)
    {
        foreach (var video in courseCreateDto.Videos)
        {
            if (_unitOfWork.MaterialRepository.Exists(
                    m => m.Title == video.Title && m.Type == "Video"))
                throw new BadRequestException($"Video ({video.Title}) already exists in the db.");

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
            if (_unitOfWork.MaterialRepository.Exists(
                m => m.Title == publication.Title && m.Type == "Publication"))
                throw new BadRequestException($"Publication ({publication.Title}) already exists in the db.");

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
            if (_unitOfWork.MaterialRepository.Exists(
                m => m.Title == article.Title && m.Type == "Article"))
                throw new BadRequestException($"Article ({article.Title}) already exists in the db.");

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
