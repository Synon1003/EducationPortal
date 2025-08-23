using EducationPortal.Data.Entities;

namespace EducationPortal.Data.Repositories.Interfaces;

public interface ICourseRepository : IRepository<Course>
{
    Task<ICollection<Course>> GetAllCoursesWithSkillsAsync();
    Task<ICollection<Course>> GetAllCoursesWithSkillsAndMaterialsAsync();
    Task<Course?> GetCourseWithSkillsAndMaterialsByIdAsync(int id);
    Task<ICollection<Course>> GetCoursesByMaterialIdAsync(int materialId);
}