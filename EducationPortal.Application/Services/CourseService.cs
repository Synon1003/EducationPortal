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

    public async Task<bool> EnrollUserOnCourseAsync(Guid userId, int courseId)
    {
        var course = await _unitOfWork.CourseRepository.GetCourseWithRelationshipsByIdAsync(courseId);
        if (course is null)
            throw new NotFoundException(nameof(Course), courseId);

        bool isInstantCompleted = course.Materials.Count == 0;

        await _unitOfWork.UserCourseRepository.InsertAsync(new UserCourse()
        {
            ProgressPercentage = isInstantCompleted ? 100 : 0,
            UserId = userId,
            CourseId = courseId
        });

        if (isInstantCompleted)
            await UpdateUserSkillsByCourseSkillsAsync(userId, course.Skills);

        await _unitOfWork.SaveChangesAsync();

        return isInstantCompleted;
    }

    public bool IsUserDoneWithMaterial(Guid userId, int materialId)
    {
        return _unitOfWork.UserMaterialRepository
            .Exists(c => c.UserId == userId && c.MaterialId == materialId);
    }

    public async Task<bool> MarkMaterialDone(Guid userId, int materialId, int courseId)
    {
        var course = await _unitOfWork.CourseRepository
            .GetCourseWithRelationshipsByIdAsync(courseId);
        if (course is null)
            throw new NotFoundException(nameof(Course), courseId);

        var userCourse = await _unitOfWork.UserCourseRepository
            .GetByFilterAsync(uc => uc.UserId == userId && uc.CourseId == courseId);
        if (userCourse is null)
            throw new NotFoundException(nameof(UserCourse), (userId, courseId));

        await _unitOfWork.UserMaterialRepository.InsertAsync(
            new UserMaterial() { UserId = userId, MaterialId = materialId });

        UpdateUserCoursePercentage(userId, userCourse, course.Materials);

        if (userCourse.IsCompleted)
            await UpdateUserSkillsByCourseSkillsAsync(userId, course.Skills);

        await _unitOfWork.SaveChangesAsync();

        return userCourse.IsCompleted;
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

    private int CountUserMaterials(Guid userId, ICollection<Material> materials)
    {
        int count = 0;
        foreach (var material in materials)
        {
            if (_unitOfWork.UserMaterialRepository
                .Exists(us => us.MaterialId == material.Id && us.UserId == userId))
                count++;
        }

        return count;
    }

    private void UpdateUserCoursePercentage(Guid userId, UserCourse userCourse, ICollection<Material> materials)
    {
        int acquiredUserMaterials = CountUserMaterials(userId, materials);
        userCourse.ProgressPercentage = 100 * (acquiredUserMaterials + 1) / materials.Count;

        _unitOfWork.UserCourseRepository.Update(userCourse);
    }

    private async Task UpdateUserSkillsByCourseSkillsAsync(Guid userId, ICollection<Skill> skills)
    {
        foreach (var skill in skills)
        {
            var userSkill = await _unitOfWork.UserSkillRepository.GetByFilterAsync(us => us.UserId == userId && us.SkillId == skill.Id);

            if (userSkill is null)
            {
                await _unitOfWork.UserSkillRepository.InsertAsync(
                    new UserSkill() { UserId = userId, SkillId = skill.Id });
            }
            else
            {
                userSkill.Level++;
                _unitOfWork.UserSkillRepository.Update(userSkill);
            }
        }
    }
}
