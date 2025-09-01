using System.Linq.Expressions;
using EducationPortal.Data.Entities;

namespace EducationPortal.Data.Repositories.Interfaces;

public interface IUserCourseRepository : IJoinRepository<UserCourse>
{
    Task<ICollection<UserCourse>> GetAllByUserIdAsync(Guid userId);
    void Update(UserCourse userCourse);
}