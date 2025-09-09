using System.Linq.Expressions;
using EducationPortal.Data.Entities;

namespace EducationPortal.Data.Repositories.Interfaces;

public interface IUserSkillRepository : IJoinRepository<UserSkill>
{
    Task<IEnumerable<UserSkill>> GetAllAsync(Expression<Func<UserSkill, bool>>? predicate = null);
    Task<ICollection<UserSkill>> GetAllByUserIdAsync(Guid userId);
    void Update(UserSkill userSkill);
}