using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Data.Entities;

namespace EducationPortal.Data.Repositories;

public class UserMaterialRepository : EntityFrameworkJoinRepository<UserMaterial>, IUserMaterialRepository
{
    public UserMaterialRepository(EducationPortalDbContext context) : base(context)
    { }
}
