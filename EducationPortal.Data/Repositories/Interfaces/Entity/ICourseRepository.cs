using EducationPortal.Data.Entities;

namespace EducationPortal.Data.Repositories.Interfaces;

public interface ICourseRepository : IRepository<Course>
{
    Task<ICollection<Course>> GetAllCoursesWithSkillsAsync();
    Task<ICollection<Course>> GetAvailableCoursesWithSkillsForUserAsync(Guid userId);
    Task<ICollection<Course>> GetInProgressCoursesWithSkillsForUserAsync(Guid userId);
    Task<ICollection<Course>> GetCompletedCoursesWithSkillsForUserAsync(Guid userId);
    Task<ICollection<Course>> GetCreatedCoursesWithSkillsForUserAsync(Guid userId);
    Task<ICollection<Course>> GetAllCoursesWithSkillsAndMaterialsAsync();
    Task<Course?> GetCourseWithRelationshipsByIdAsync(int id);
    Task<ICollection<Course>> GetCoursesByMaterialIdAsync(int materialId);
}