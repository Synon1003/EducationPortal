using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EducationPortal.Data.Repositories;

public class UserSkillRepository : EntityFrameworkJoinRepository<UserSkill>, IUserSkillRepository
{
    public UserSkillRepository(EducationPortalDbContext context) : base(context)
    { }

    public async Task<IEnumerable<UserSkill>> GetAllAsync(Expression<Func<UserSkill, bool>>? predicate = null)
    {
        IQueryable<UserSkill> query = _context.UserSkills;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return await query.ToListAsync();
    }

    public async Task<ICollection<UserSkill>> GetAllByUserIdAsync(Guid userId) =>
        await _context.UserSkills.Include(us => us.Skill).Where(us => us.UserId == userId).ToListAsync();

    public void Update(UserSkill userSkill)
    {
        _context.UserSkills.Update(userSkill);
    }
}
