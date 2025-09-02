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

        await AddCourseSkills(course, courseCreateDto);
        AddCourseVideos(course, courseCreateDto);
        AddCoursePublications(course, courseCreateDto);
        AddCourseArticles(course, courseCreateDto);
        await AddLoadedMaterials(course, courseCreateDto);
        await _unitOfWork.CourseRepository.InsertAsync(course);
        await _unitOfWork.SaveChangesAsync();

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
                    new UserSkill() { UserId = userId, SkillId = skill.Id, Level = 1 });
            }
            else
            {
                userSkill.Level++;
                _unitOfWork.UserSkillRepository.Update(userSkill);
            }
        }
    }
}
