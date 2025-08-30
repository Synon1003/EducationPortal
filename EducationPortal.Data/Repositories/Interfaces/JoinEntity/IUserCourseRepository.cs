using System.Linq.Expressions;
using EducationPortal.Data.Entities;

namespace EducationPortal.Data.Repositories.Interfaces;

public interface IUserCourseRepository : IJoinRepository<UserCourse>
{
    void Update(UserCourse userCourse);
}