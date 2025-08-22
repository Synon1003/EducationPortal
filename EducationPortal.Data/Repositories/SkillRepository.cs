using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Data.Entities;

namespace EducationPortal.Data.Repositories;

public class SkillRepository : EntityFrameworkRepository<Skill>, ISkillRepository
{
    public SkillRepository(EducationPortalDbContext context) : base(context)
    { }
}
