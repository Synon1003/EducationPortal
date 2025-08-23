using EducationPortal.Application.Dtos;

namespace EducationPortal.Application.Services.Interfaces;

public interface ICourseService
{
    Task<ICollection<CourseListDto>> GetAllCoursesWithSkillsAsync();
    Task<ICollection<CourseListDto>> GetCoursesByMaterialIdAsync(int materialId);
    Task<CourseDetailDto> GetCourseWithSkillsAndMaterialsByIdAsync(int id);
    // Task<CourseDto> AddCourseAsync(CreateCourseDto courseDto);
}