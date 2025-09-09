using EducationPortal.Data.Entities;
using EducationPortal.Data.Helpers;

namespace EducationPortal.Data.Repositories.Interfaces;

public interface IUserCourseRepository : IJoinRepository<UserCourse>
{
    Task<List<UserCourse>> GetAllAsync(UserCoursesFilter filter);
    void Update(UserCourse userCourse);
    void Delete(UserCourse userCourse);
}