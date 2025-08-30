using EducationPortal.Data.Entities;

namespace EducationPortal.Data.Repositories.Interfaces;

public interface IUserSkillRepository : IJoinRepository<UserSkill>
{
    void Update(UserSkill userSkill);
    Task<ICollection<UserSkill>> GetAllByUserIdAsync(Guid userId);
}