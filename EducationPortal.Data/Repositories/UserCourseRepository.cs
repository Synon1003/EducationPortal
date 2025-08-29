using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EducationPortal.Data.Repositories;

public class UserCourseRepository : IUserCourseRepository
{
    protected EducationPortalDbContext _context;

    public UserCourseRepository(EducationPortalDbContext context)
    {
        _context = context;
    }

    public bool Exists(Func<UserCourse, bool> predicate)
    {
        return _context.Set<UserCourse>().AsNoTracking().Any(predicate);
    }

    public async Task<UserCourse?> GetByFilterAsync(Expression<Func<UserCourse, bool>> predicate)
    {
        return await _context.Set<UserCourse>().AsNoTracking().Where(predicate).SingleOrDefaultAsync();
    }

    public async Task InsertAsync(UserCourse userCourse)
    {
        await _context.Set<UserCourse>().AddAsync(userCourse);
    }
}
