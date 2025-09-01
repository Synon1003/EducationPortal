using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.Data.Repositories;

public class UserCourseRepository : EntityFrameworkJoinRepository<UserCourse>, IUserCourseRepository
{
    public UserCourseRepository(EducationPortalDbContext context) : base(context)
    { }

    public async Task<ICollection<UserCourse>> GetAllByUserIdAsync(Guid userId) =>
        await _context.UserCourses.AsNoTracking()
            .Where(us => us.UserId == userId)
            .ToListAsync();
}
