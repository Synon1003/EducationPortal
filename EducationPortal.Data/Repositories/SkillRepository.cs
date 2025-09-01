using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.Data.Repositories;

public class SkillRepository : EntityFrameworkRepository<Skill>, ISkillRepository
{
    public SkillRepository(EducationPortalDbContext context) : base(context)
    { }

    public async Task<ICollection<Skill>> GetSkillsByCourseIdAsync(int courseId) =>
        await _context.Skills.AsNoTracking()
            .Where(m => m.Courses.Any(c => c.Id == courseId)).ToListAsync();
}
