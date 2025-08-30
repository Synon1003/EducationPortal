using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Data.Entities;

namespace EducationPortal.Data.Repositories;

public class UserCourseRepository : EntityFrameworkJoinRepository<UserCourse>, IUserCourseRepository
{
    public UserCourseRepository(EducationPortalDbContext context) : base(context)
    { }
}
