using Moq;

using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Data.Entities;
using System.Linq.Expressions;

namespace EducationPortal.Tests.Mocks;

public static class MockUserCourseRepository
{
    public static Mock<IUserCourseRepository> GetUserCourseRepository()
    {
        var userCourses = new List<UserCourse>
        {
            new UserCourse
            {
                Id = 1,
                UserId = new Guid(),
                CourseId = 1,
            },
            new UserCourse
            {
                Id = 2,
                UserId = new Guid(),
                CourseId = 2,
                ProgressPercentage = 50
            }
        };

        var mockRepository = new Mock<IUserCourseRepository>();

        mockRepository.Setup(u => u.GetAllByUserIdAsync(It.IsAny<Guid>()))
        .ReturnsAsync([userCourses.Last()]);

        mockRepository.Setup(uc => uc.GetByFilterAsync(It.IsAny<Expression<Func<UserCourse, bool>>>()))
            .ReturnsAsync((Expression<Func<UserCourse, bool>> filter) =>
            {
                var testEntity = new UserCourse { UserId = userCourses.First().UserId, CourseId = userCourses.First().CourseId };

                return filter.Compile().Invoke(testEntity) ? testEntity : null;
            });

        return mockRepository;
    }
}