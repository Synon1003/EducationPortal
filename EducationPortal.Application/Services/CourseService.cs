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
            "inprogress" => await _unitOfWork.CourseRepository.GetInProgressCoursesWithSkillsForUserAsync(userId),
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

    public async Task<List<string>> GetCourseCreateValidationErrorsAsync(CourseCreateDto courseCreateDto)
    {
        List<string> validationErrors = [];

        if (await _unitOfWork.CourseRepository.ExistsAsync(c => c.Name == courseCreateDto.Name))
            validationErrors.Add($"CourseName({courseCreateDto.Name})IsAlreadyTaken");

        await CheckSkillCreateValidationErrorsAsync(courseCreateDto.Skills, validationErrors);
        await CheckVideoCreateValidationErrorsAsync(courseCreateDto.Videos, validationErrors);
        await CheckPublicationCreateValidationErrorsAsync(courseCreateDto.Publications, validationErrors);
        await CheckArticleCreateValidationErrorsAsync(courseCreateDto.Articles, validationErrors);

        return validationErrors;
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
        _unitOfWork.CourseRepository.Insert(course);
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

        _unitOfWork.UserCourseRepository.Insert(new UserCourse()
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

    public async Task LeaveCourseAsync(Guid userId, int courseId)
    {
        var userCourse = await _unitOfWork.UserCourseRepository
            .GetByFilterAsync(c => c.CourseId == courseId && c.UserId == userId);
        if (userCourse is null)
            throw new NotFoundException(nameof(UserCourse), (userId, courseId));

        _unitOfWork.UserCourseRepository.Delete(userCourse);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> IsUserDoneWithMaterialAsync(Guid userId, int materialId)
    {
        return await _unitOfWork.UserMaterialRepository
            .ExistsAsync(c => c.UserId == userId && c.MaterialId == materialId);
    }

    public async Task<bool> MarkMaterialDoneAsync(Guid userId, int materialId, int courseId)
    {
        var userCourses = await _unitOfWork.UserCourseRepository.GetAllByUserIdAsync(userId);

        _unitOfWork.UserMaterialRepository.Insert(
            new UserMaterial() { UserId = userId, MaterialId = materialId });

        var materialRelatedCourses = await _unitOfWork.CourseRepository.GetCoursesByMaterialIdAsync(materialId);


        foreach (var relatedCourse in materialRelatedCourses)
        {
            foreach (var userCourse in userCourses)
            {
                if (relatedCourse.Id == userCourse.CourseId && userCourse.ProgressPercentage != 100)
                {
                    await UpdateUserCoursePercentageAsync(userId, userCourse, relatedCourse.Materials);

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

        var existingSkillIds = courseCreateDto.LoadedSkills
            .Select(s => s.Id)
            .ToList();

        if (existingSkillIds.Any())
        {
            var existingSkills = await _unitOfWork.SkillRepository
                .GetAllAsync(s => existingSkillIds.Contains(s.Id));

            foreach (var skill in existingSkills)
            {
                course.Skills.Add(skill);
            }
        }
    }

    private async Task AddLoadedMaterials(Course course, CourseCreateDto courseCreateDto)
    {
        var materialIds = courseCreateDto.LoadedVideos.Select(v => v.Id)
            .Concat(courseCreateDto.LoadedPublications.Select(p => p.Id))
            .Concat(courseCreateDto.LoadedArticles.Select(a => a.Id))
            .ToList();

        if (materialIds.Count == 0)
            return;

        var materials = await _unitOfWork.MaterialRepository
            .GetAllAsync(m => materialIds.Contains(m.Id));

        foreach (var material in materials)
            course.Materials.Add(material);
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

    private async Task<int> CountUserMaterialsAsync(Guid userId, ICollection<Material> materials)
    {
        int count = 0;
        foreach (var material in materials)
        {
            if (await _unitOfWork.UserMaterialRepository
                .ExistsAsync(us => us.MaterialId == material.Id && us.UserId == userId))
                count++;
        }

        return count;
    }

    private async Task UpdateUserCoursePercentageAsync(Guid userId, UserCourse userCourse, ICollection<Material> materials)
    {
        int acquiredUserMaterials = await CountUserMaterialsAsync(userId, materials);
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
                _unitOfWork.UserSkillRepository.Insert(
                    new UserSkill() { UserId = userId, SkillId = skill.Id, Level = 1 });
            }
            else
            {
                userSkill.Level++;
                _unitOfWork.UserSkillRepository.Update(userSkill);
            }
        }
    }

    private async Task CheckSkillCreateValidationErrorsAsync(
        List<SkillCreateDto> skills, List<string> validationErrors)
    {
        var duplicateSkillNames = skills
            .GroupBy(s => s.Name).Where(g => g.Count() > 1).Select(g => g.Key);

        foreach (var skill in duplicateSkillNames)
            validationErrors.Add($"SkillName({skill})IsDuplicated");

        foreach (var skill in skills)
            if (await _unitOfWork.SkillRepository.ExistsAsync(s => s.Name == skill.Name))
                validationErrors.Add($"SkillName({skill.Name})IsAlreadyTaken");
    }

    private async Task CheckVideoCreateValidationErrorsAsync(
        List<VideoCreateDto> videos, List<string> validationErrors)
    {
        var duplicateVideoTitles = videos
            .GroupBy(v => v.Title).Where(g => g.Count() > 1).Select(g => g.Key);

        foreach (var video in duplicateVideoTitles)
            validationErrors.Add($"VideoTitle({video})IsDuplicated");

        foreach (var video in videos)
            if (await _unitOfWork.MaterialRepository.ExistsAsync(
                m => m.Title == video.Title && m.Type == "Video"))
                validationErrors.Add($"VideoTitle({video.Title})IsAlreadyTaken");
    }

    private async Task CheckPublicationCreateValidationErrorsAsync(
        List<PublicationCreateDto> publications, List<string> validationErrors)
    {
        var duplicatePublicationTitles = publications
            .GroupBy(v => v.Title).Where(g => g.Count() > 1).Select(g => g.Key);

        foreach (var publication in duplicatePublicationTitles)
            validationErrors.Add($"PublicationTitle({publication})IsDuplicated");

        foreach (var publication in publications)
            if (await _unitOfWork.MaterialRepository.ExistsAsync(
                m => m.Title == publication.Title && m.Type == "Publication"))
                validationErrors.Add($"PublicationTitle({publication.Title})IsAlreadyTaken");
    }

    private async Task CheckArticleCreateValidationErrorsAsync(
        List<ArticleCreateDto> articles, List<string> validationErrors)
    {
        var duplicateArticleTitles = articles
            .GroupBy(v => v.Title).Where(g => g.Count() > 1).Select(g => g.Key);

        foreach (var article in duplicateArticleTitles)
            validationErrors.Add($"ArticleTitle({article})IsDuplicated");

        foreach (var article in articles)
            if (await _unitOfWork.MaterialRepository.ExistsAsync(
                m => m.Title == article.Title && m.Type == "Article"))
                validationErrors.Add($"ArticleTitle({article.Title})IsAlreadyTaken");
    }
}
