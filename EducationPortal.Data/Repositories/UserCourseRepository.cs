using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Data.Entities;
using EducationPortal.Data.Helpers;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.Data.Repositories;

public class UserCourseRepository : EntityFrameworkJoinRepository<UserCourse>, IUserCourseRepository
{
    public UserCourseRepository(EducationPortalDbContext context) : base(context)
    { }

    public async Task<List<UserCourse>> GetAllAsync(UserCoursesFilter filter)
    {
        var query = _context.UserCourses.AsNoTracking();

        if (filter.MaterialId.HasValue)
        {
            query = query.Where(us => us.Course!.Materials.Any(m => m.Id == filter.MaterialId.Value));
        }

        if (filter.UserId.HasValue)
        {
            query = query.Where(us => us.UserId == filter.UserId.Value);
        }

        if (filter.IsCompleted.HasValue)
        {
            if (filter.IsCompleted.Value)
                query = query.Where(us => us.ProgressPercentage >= 100);
            else
                query = query.Where(us => us.ProgressPercentage < 100);
        }

        return await query.ToListAsync();
    }

    public void Update(UserCourse userCourse)
    {
        _context.UserCourses.Update(userCourse);
    }

    public void Delete(UserCourse userCourse)
    {
        _context.UserCourses.Remove(userCourse);
    }
}
