using System.Linq.Expressions;
using EducationPortal.Data.Entities;

namespace EducationPortal.Data.Repositories.Interfaces;

public interface IUserCourseRepository
{
    bool Exists(Func<UserCourse, bool> predicate);
    Task<UserCourse?> GetByFilterAsync(Expression<Func<UserCourse, bool>> predicate);
    Task InsertAsync(UserCourse userCourse);
}