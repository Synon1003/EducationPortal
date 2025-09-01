using EducationPortal.Data.Entities;

namespace EducationPortal.Data.Repositories.Interfaces;

public interface IUserSkillRepository : IJoinRepository<UserSkill>
{
    Task<ICollection<UserSkill>> GetAllByUserIdAsync(Guid userId);
    void Update(UserSkill userSkill);
}