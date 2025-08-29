using EducationPortal.Application.Dtos;

namespace EducationPortal.Application.Services.Interfaces;

public interface ICourseService
{
    Task<ICollection<CourseListDto>> GetAllCoursesWithSkillsAsync();
    Task<ICollection<CourseListDto>> GetCoursesByMaterialIdAsync(int materialId);
    Task<CourseListDto> GetCourseByIdAsync(int id);
    Task<CourseDetailDto> GetCourseWithRelationshipsByIdAsync(int id);
    Task<CourseDetailDto> CreateCourseAsync(CourseCreateDto courseCreateDto);

    bool IsUserEnrolledOnCourse(Guid userId, int courseId);
    Task<UserCourseDto?> GetUserCourseAsync(Guid userId, int courseId);
    Task EnrollUserOnCourseAsync(Guid userId, int courseId);
}