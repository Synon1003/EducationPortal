using AutoMapper;
using EducationPortal.Data.Entities;
using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Application.Dtos;
using EducationPortal.Application.Services.Interfaces;
using EducationPortal.Application.Exceptions;
using Microsoft.Extensions.Logging;

namespace EducationPortal.Application.Services;

public class CourseService : ICourseService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CourseService> _logger;

    public CourseService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<CourseService> logger
    )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
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

    public void CheckCourseCreateValidationErrors(
        CourseCreateDto courseCreateDto, out List<string> validationErrors)
    {
        validationErrors = [];

        if (_unitOfWork.CourseRepository.Exists(c => c.Name == courseCreateDto.Name))
            validationErrors.Add($"Course name ({courseCreateDto.Name}) is already taken.");

        CheckSkillCreateValidationErrors(courseCreateDto.Skills, validationErrors);
        CheckVideoCreateValidationErrors(courseCreateDto.Videos, validationErrors);
        CheckPublicationCreateValidationErrors(courseCreateDto.Publications, validationErrors);
        CheckArticleCreateValidationErrors(courseCreateDto.Articles, validationErrors);
    }

    public async Task<CourseDetailDto> CreateCourseAsync(CourseCreateDto courseCreateDto)
    {
        Course course = new Course
        {
            Name = courseCreateDto.Name,
            Description = courseCreateDto.Description,
            CreatedBy = courseCreateDto.CreatedBy!.Value
        };

        await AddCourseSkills(course, courseCreateDto);
        AddCourseVideos(course, courseCreateDto);
        AddCoursePublications(course, courseCreateDto);
        AddCourseArticles(course, courseCreateDto);
        await AddLoadedMaterials(course, courseCreateDto);
        await _unitOfWork.CourseRepository.InsertAsync(course);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("<User Id={userId}> created <Course Id={courseId} Name={courseName}> done",
            course.CreatedBy, course.Id, course.Name);

        return _mapper.Map<CourseDetailDto>(course);
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

        (int completedCount, int totalCount) = await GetCountersFromMaterialRatioAsync(userId, course);
        bool isInstantCompleted = completedCount == totalCount;

        await _unitOfWork.UserCourseRepository.InsertAsync(new UserCourse()
        {
            ProgressPercentage = totalCount == 0 ? 0 : 100 * completedCount / totalCount,
            UserId = userId,
            CourseId = courseId
        });

        if (isInstantCompleted)
            await UpdateUserSkillsByCourseSkillsAsync(userId, course.Skills);

        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("<User Id={userId}> enrolled on <Course Id={courseId} Name={courseName}>",
            userId, course.Id, course.Name);

        return isInstantCompleted;
    }

    public bool IsUserDoneWithMaterial(Guid userId, int materialId)
    {
        return _unitOfWork.UserMaterialRepository
            .Exists(c => c.UserId == userId && c.MaterialId == materialId);
    }

    public async Task<bool> MarkMaterialDone(Guid userId, int materialId, int courseId)
    {
        var userCourses = await _unitOfWork.UserCourseRepository.GetAllByUserIdAsync(userId);

        await _unitOfWork.UserMaterialRepository.InsertAsync(
            new UserMaterial() { UserId = userId, MaterialId = materialId });

        var materialRelatedCourses = await _unitOfWork.CourseRepository.GetCoursesByMaterialIdAsync(materialId);


        foreach (var relatedCourse in materialRelatedCourses)
        {
            foreach (var userCourse in userCourses)
            {
                if (relatedCourse.Id == userCourse.CourseId && userCourse.ProgressPercentage != 100)
                {
                    UpdateUserCoursePercentage(userId, userCourse, relatedCourse.Materials);

                    if (userCourse.IsCompleted)
                    {
                        var relatedSkills = await _unitOfWork.SkillRepository.GetSkillsByCourseIdAsync(relatedCourse.Id);

                        await UpdateUserSkillsByCourseSkillsAsync(userId, relatedSkills);
                    }
                }
            }
        }

        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("<User Id={userId}> marked <Material Id={Id}> done", userId, materialId);

        return userCourses.First(uc => uc.CourseId == courseId).IsCompleted;
    }

    private async Task<(int, int)> GetCountersFromMaterialRatioAsync(Guid userId, Course course)
    {
        var userMaterials = await _unitOfWork.UserMaterialRepository
        .GetAllByUserIdAsync(userId);

        var userMaterialIds = userMaterials.Select(um => um.MaterialId).ToList();
        var courseMaterialIds = course.Materials.Select(m => m.Id).ToList();

        int completedCount = courseMaterialIds.Intersect(userMaterialIds).Count();
        int totalCount = courseMaterialIds.Count;

        return (completedCount, totalCount);
    }

    private async Task AddCourseSkills(Course course, CourseCreateDto courseCreateDto)
    {
        foreach (var skill in courseCreateDto.Skills)
            course.Skills.Add(new Skill { Name = skill.Name });

        foreach (var skill in courseCreateDto.LoadedSkills)
        {
            course.Skills.Add((await _unitOfWork.SkillRepository.GetByIdAsync(skill.Id))!);
        }
    }

    private async Task AddLoadedMaterials(Course course, CourseCreateDto courseCreateDto)
    {
        foreach (var video in courseCreateDto.LoadedVideos)
        {
            course.Materials.Add((await _unitOfWork.MaterialRepository.GetByIdAsync(video.Id))!);
        }

        foreach (var publication in courseCreateDto.LoadedPublications)
        {
            course.Materials.Add((await _unitOfWork.MaterialRepository.GetByIdAsync(publication.Id))!);
        }

        foreach (var article in courseCreateDto.LoadedArticles)
        {
            course.Materials.Add((await _unitOfWork.MaterialRepository.GetByIdAsync(article.Id))!);
        }
    }

    private void AddCourseVideos(Course course, CourseCreateDto courseCreateDto)
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

    private void AddCourseArticles(Course course, CourseCreateDto courseCreateDto)
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
                    new UserSkill() { UserId = userId, SkillId = skill.Id, Level = 1 });
            }
            else
            {
                userSkill.Level++;
                _unitOfWork.UserSkillRepository.Update(userSkill);
            }
        }
    }

    private void CheckSkillCreateValidationErrors(
        List<SkillCreateDto> skills, List<string> validationErrors)
    {
        var duplicateSkillNames = skills
            .GroupBy(s => s.Name).Where(g => g.Count() > 1).Select(g => g.Key);

        foreach (var skill in duplicateSkillNames)
            validationErrors.Add($"Skill name ({skill}) is duplicated.");

        foreach (var skill in skills)
            if (_unitOfWork.SkillRepository.Exists(s => s.Name == skill.Name))
                validationErrors.Add($"Skill name ({skill.Name}) is already taken.");
    }

    private void CheckVideoCreateValidationErrors(
        List<VideoCreateDto> videos, List<string> validationErrors)
    {
        var duplicateVideoTitles = videos
            .GroupBy(v => v.Title).Where(g => g.Count() > 1).Select(g => g.Key);

        foreach (var video in duplicateVideoTitles)
            validationErrors.Add($"Video title ({video}) is duplicated.");

        foreach (var video in videos)
            if (_unitOfWork.MaterialRepository.Exists(
                m => m.Title == video.Title && m.Type == "Video"))
                validationErrors.Add($"Video title ({video.Title}) is already taken.");
    }

    private void CheckPublicationCreateValidationErrors(
        List<PublicationCreateDto> publications, List<string> validationErrors)
    {
        var duplicatePublicationTitles = publications
            .GroupBy(v => v.Title).Where(g => g.Count() > 1).Select(g => g.Key);

        foreach (var publication in duplicatePublicationTitles)
            validationErrors.Add($"Publication title ({publication}) is duplicated.");

        foreach (var publication in publications)
            if (_unitOfWork.MaterialRepository.Exists(
                m => m.Title == publication.Title && m.Type == "Publication"))
                validationErrors.Add($"Publication title ({publication.Title}) is already taken.");
    }

    private void CheckArticleCreateValidationErrors(
        List<ArticleCreateDto> articles, List<string> validationErrors)
    {
        var duplicateArticleTitles = articles
            .GroupBy(v => v.Title).Where(g => g.Count() > 1).Select(g => g.Key);

        foreach (var article in duplicateArticleTitles)
            validationErrors.Add($"Article title ({article}) is duplicated.");

        foreach (var article in articles)
            if (_unitOfWork.MaterialRepository.Exists(
                m => m.Title == article.Title && m.Type == "Article"))
                validationErrors.Add($"Article title ({article.Title}) is already taken.");
    }
}
