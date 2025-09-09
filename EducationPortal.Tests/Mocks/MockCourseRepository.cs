using Moq;

using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Data.Entities;
using System.Linq.Expressions;

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
                Name = "TestCourseName1",
                Description = "TestCourseDescription1",
                Skills = new List<Skill> {
                    new Skill { Id = 1, Name = "Skill1" },
                    new Skill { Id = 2, Name = "Skill2" }
                },
                Materials = new List<Material> {
                    new Material { Id = 1, Title = "Material1", Type = "Video" },
                }
            },
            new Course
            {
                Id = 2,
                Name = "TestCourseName2",
                Description = "TestCourseDescription2",
                Skills = new List<Skill> {
                    new Skill { Id = 1, Name = "Skill1" },
                    new Skill { Id = 2, Name = "Skill2" }
                },
                Materials = new List<Material> {
                    new Material { Id = 1, Title = "Material1", Type = "Video"},
                    new Material { Id = 2, Title = "Material2", Type = "Article"},
                }
            }
        };
        var mockRepository = new Mock<ICourseRepository>();


        mockRepository.Setup(r => r.GetAllCoursesWithSkillsAsync()).ReturnsAsync(courses);

        mockRepository.Setup(r => r.GetAvailableCoursesWithSkillsForUserAsync(It.IsAny<Guid>())).ReturnsAsync(courses);
        mockRepository.Setup(r => r.GetInProgressCoursesWithSkillsForUserAsync(It.IsAny<Guid>())).ReturnsAsync(courses);
        mockRepository.Setup(r => r.GetCompletedCoursesWithSkillsForUserAsync(It.IsAny<Guid>())).ReturnsAsync(courses);
        mockRepository.Setup(r => r.GetCreatedCoursesWithSkillsForUserAsync(It.IsAny<Guid>())).ReturnsAsync(courses);


        mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => id == 0 ? null : courses.First(c => c.Id == id));
        mockRepository.Setup(r => r.GetCourseWithRelationshipsByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => id == 0 ? null : courses.First(c => c.Id == id));

        mockRepository.Setup(r => r.ExistsAsync(It.IsAny<Expression<Func<Course, bool>>>()))
            .ReturnsAsync((Expression<Func<Course, bool>> predicate) =>
                predicate.Compile()(new Course { Name = "ExistingCourseName" }));

        mockRepository.Setup(u => u.GetCoursesByMaterialIdAsync(
            It.IsAny<int>())).ReturnsAsync((int id) => id == 2 ? [courses.First()] : [courses.Last()]);

        return mockRepository;
    }
}