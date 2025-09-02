using EducationPortal.Application.Dtos;

namespace EducationPortal.Application.Services.Interfaces;

public interface ICourseService
{
    Task<ICollection<CourseListDto>> GetFilteredCoursesWithSkillsAsync(Guid userId, string filter);
    Task<CourseListDto> GetCourseByIdAsync(int id);
    Task<CourseDetailDto> GetCourseWithRelationshipsByIdAsync(int id);
    Task<CourseDetailDto> CreateCourseAsync(CourseCreateDto courseCreateDto);

    Task<UserCourseDto?> GetUserCourseAsync(Guid userId, int courseId);
    bool IsUserDoneWithMaterial(Guid userId, int materialId);
    Task<bool> EnrollUserOnCourseAsync(Guid userId, int courseId);
    Task<bool> MarkMaterialDone(Guid userId, int materialId, int courseId);
}