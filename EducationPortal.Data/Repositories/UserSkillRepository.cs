using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Data.Entities;

namespace EducationPortal.Data.Repositories;

public class UserSkillRepository : EntityFrameworkJoinRepository<UserSkill>, IUserSkillRepository
{
    public UserSkillRepository(EducationPortalDbContext context) : base(context)
    { }
}
