using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EducationPortal.Data.Repositories;

public class UserMaterialRepository : IUserMaterialRepository
{
    protected EducationPortalDbContext _context;

    public UserMaterialRepository(EducationPortalDbContext context)
    {
        _context = context;
    }

    public bool Exists(Func<UserMaterial, bool> predicate)
    {
        return _context.Set<UserMaterial>().AsNoTracking().Any(predicate);
    }

    public async Task<UserMaterial?> GetByFilterAsync(Expression<Func<UserMaterial, bool>> predicate)
    {
        return await _context.Set<UserMaterial>().AsNoTracking().Where(predicate).FirstOrDefaultAsync();
    }

    public async Task InsertAsync(UserMaterial userMaterial)
    {
        await _context.Set<UserMaterial>().AddAsync(userMaterial);
    }
}
