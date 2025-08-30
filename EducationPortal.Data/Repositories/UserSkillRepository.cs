using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.Data.Repositories;

public class UserSkillRepository : EntityFrameworkJoinRepository<UserSkill>, IUserSkillRepository
{
    public UserSkillRepository(EducationPortalDbContext context) : base(context)
    { }

    public async Task<ICollection<UserSkill>> GetAllByUserIdAsync(Guid userId) =>
        await _context.UserSkills.Include(us => us.Skill).Where(us => us.UserId == userId).ToListAsync();
}
