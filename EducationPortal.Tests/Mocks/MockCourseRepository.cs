using Moq;

using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Data.Entities;

namespace EducationPortal.Tests.Mocks;

public static class MockCourseRepository
{
    public static Mock<ICourseRepository> GetCourseRepository()
    {
        var courses = new List<Course>
        {
            new Course
            {
                Id = 1,
                Name = "TestCourseName",
                Description = "TestCourseDescription",
                Skills = new List<Skill> {
                    new Skill { Id = 1, Name = "Skill1" },
                    new Skill { Id = 2, Name = "Skill2" }
                }
            }
        };
        var mockRepo = new Mock<ICourseRepository>();


        mockRepo.Setup(r => r.GetAllCoursesWithSkillsAsync()).ReturnsAsync(courses);

        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => id == 0 ? null : courses.First());

        mockRepo.Setup(r => r.InsertAsync(It.IsAny<Course>())).Returns(async (Course course) =>
        {
            courses.Add(course);
            await Task.CompletedTask;
        });

        return mockRepo;
    }
}