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

    public async Task<ICollection<CourseListDto>> GetFilteredCoursesWithSkillsAsync(Guid userId, string filter)
    {
        var courses = filter switch
        {
            "available" => await _unitOfWork.CourseRepository.GetAvailableCoursesWithSkillsForUserAsync(userId),
            "in-progress" => await _unitOfWork.CourseRepository.GetInProgressCoursesWithSkillsForUserAsync(userId),
            "completed" => await _unitOfWork.CourseRepository.GetCompletedCoursesWithSkillsForUserAsync(userId),
            "created" => await _unitOfWork.CourseRepository.GetCreatedCoursesWithSkillsForUserAsync(userId),
            _ => await _unitOfWork.CourseRepository.GetAllCoursesWithSkillsAsync(),
        };

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
        if (courseCreateDto.CreatedBy is null)
            throw new BadRequestException($"User id ({courseCreateDto.CreatedBy}) cannot be null.");

        CheckValidationErrors(courseCreateDto, out List<string> validationErrors);
        if (validationErrors.Any())
            throw new ValidationException(validationErrors);

        Course course = new Course
        {
            Name = courseCreateDto.Name,
            Description = courseCreateDto.Description,
            CreatedBy = courseCreateDto.CreatedBy!.Value
        };

        AddCourseSkills(course, courseCreateDto);
        AddCourseVideos(course, courseCreateDto);
        AddCoursePublications(course, courseCreateDto);
        AddCourseArticles(course, courseCreateDto);
        await _unitOfWork.CourseRepository.InsertAsync(course);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<CourseDetailDto>(course);
    }

    public bool IsUserEnrolledOnCourse(Guid userId, int courseId)
    {
        return _unitOfWork.UserCourseRepository
            .Exists(c => c.UserId == userId && c.CourseId == courseId);
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

    private void AddCourseSkills(Course course, CourseCreateDto courseCreateDto)
    {
        foreach (var skill in courseCreateDto.Skills)
            course.Skills.Add(new Skill { Name = skill.Name });
    }

    private void AddCourseVideos(
        Course course, CourseCreateDto courseCreateDto)
    {
        foreach (var video in courseCreateDto.Videos)
        {
            course.Materials.Add(new Video
            {
                Title = video.Title,
                Duration = video.Duration,
                Quality = video.Quality
            });
        }
    }

    private void AddCoursePublications(
        Course course, CourseCreateDto courseCreateDto)
    {
        foreach (var publication in courseCreateDto.Publications)
        {
            course.Materials.Add(new Publication
            {
                Title = publication.Title,
                Authors = publication.Authors,
                Format = publication.Format,
                Pages = publication.Pages,
                PublicationYear = publication.PublicationYear
            });
        }
    }

    private void AddCourseArticles(
        Course course, CourseCreateDto courseCreateDto)
    {
        foreach (var article in courseCreateDto.Articles)
        {
            course.Materials.Add(new Article
            {
                Title = article.Title,
                PublicationDate = article.PublicationDate,
                ResourceLink = article.ResourceLink
            });
        }
    }

    private void CheckValidationErrors(CourseCreateDto courseCreateDto, out List<string> validationErrors)
    {
        validationErrors = new List<string>();

        if (_unitOfWork.CourseRepository.Exists(c => c.Name == courseCreateDto.Name))
            validationErrors.Add($"Course ({courseCreateDto.Name}) already exists in the db.");

        foreach (var skill in courseCreateDto.Skills)
            if (_unitOfWork.SkillRepository.Exists(s => s.Name == skill.Name))
                validationErrors.Add($"Skill ({skill.Name}) already exists in the db.");

        foreach (var video in courseCreateDto.Videos)
            if (_unitOfWork.MaterialRepository.Exists(
                m => m.Title == video.Title && m.Type == "Video"))
                validationErrors.Add($"Video ({video.Title}) already exists in the db.");

        foreach (var publication in courseCreateDto.Publications)
            if (_unitOfWork.MaterialRepository.Exists(
                m => m.Title == publication.Title && m.Type == "Publication"))
                validationErrors.Add($"Publication ({publication.Title}) already exists in the db.");

        foreach (var article in courseCreateDto.Articles)
            if (_unitOfWork.MaterialRepository.Exists(
                m => m.Title == article.Title && m.Type == "Article"))
                validationErrors.Add($"Article ({article.Title}) already exists in the db.");
    }
}
