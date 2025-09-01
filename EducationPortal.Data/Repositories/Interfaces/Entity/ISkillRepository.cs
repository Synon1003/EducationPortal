using EducationPortal.Data.Entities;

namespace EducationPortal.Data.Repositories.Interfaces;

public interface ISkillRepository : IRepository<Skill>
{
    Task<ICollection<Skill>> GetSkillsByCourseIdAsync(int courseId);
    Task<ICollection<Skill>> GetAllSkillsAsync();
}