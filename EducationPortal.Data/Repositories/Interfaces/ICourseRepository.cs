using EducationPortal.Data.Entities;

namespace EducationPortal.Data.Repositories.Interfaces;

public interface ICourseRepository : IRepository<Course>
{
    Task<ICollection<Course>>
    GetAllCoursesWithSkillsAndMaterialsAsync();
    Task<Course?> GetCourseWithSkillsAsync(int id);
    Task<ICollection<Course>> GetAstronautsByMaterialIdAsync(int materialId);
}