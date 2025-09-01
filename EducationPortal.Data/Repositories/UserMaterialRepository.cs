using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.Data.Repositories;

public class UserMaterialRepository : EntityFrameworkJoinRepository<UserMaterial>, IUserMaterialRepository
{
    public UserMaterialRepository(EducationPortalDbContext context) : base(context)
    { }

    public async Task<ICollection<UserMaterial>> GetAllByUserIdAsync(Guid userId) =>
        await _context.UserMaterials.AsNoTracking().Where(us => us.UserId == userId).ToListAsync();
}
