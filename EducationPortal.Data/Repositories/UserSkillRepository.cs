using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EducationPortal.Data.Repositories;

public class UserSkillRepository : IUserSkillRepository
{
    protected EducationPortalDbContext _context;

    public UserSkillRepository(EducationPortalDbContext context)
    {
        _context = context;
    }

    public bool Exists(Func<UserSkill, bool> predicate)
    {
        return _context.Set<UserSkill>().AsNoTracking().Any(predicate);
    }

    public async Task<UserSkill?> GetByFilterAsync(Expression<Func<UserSkill, bool>> predicate)
    {
        return await _context.Set<UserSkill>().AsNoTracking().Where(predicate).SingleOrDefaultAsync();
    }

    public async Task InsertAsync(UserSkill userSkill)
    {
        await _context.Set<UserSkill>().AddAsync(userSkill);
    }

    public void Update(UserSkill userSkill)
    {
        _context.Set<UserSkill>().Update(userSkill);
    }
}
