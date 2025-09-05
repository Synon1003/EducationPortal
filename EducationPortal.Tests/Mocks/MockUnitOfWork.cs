using Moq;

using EducationPortal.Data.Repositories.Interfaces;

namespace EducationPortal.Tests.Mocks;

public static class MockUnitOfWork
{
    public static Mock<IUnitOfWork> GetUnitOfWork()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockCourseRepository = MockCourseRepository.GetCourseRepository();
        var mockUserCourseRepository = MockUserCourseRepository.GetUserCourseRepository();
        var mockUserMaterialRepository = MockUserMaterialRepository.GetUserMaterialRepository();
        var mockUserSkillRepository = MockUserSkillRepository.GetUserSkillRepository();
        var mockSkillRepository = MockSkillRepository.GetSkillRepository();
        var mockMaterialRepository = MockMaterialRepository.GetMaterialRepository();

        mockUnitOfWork.Setup(r => r.CourseRepository).Returns(mockCourseRepository.Object);
        mockUnitOfWork.Setup(r => r.UserCourseRepository).Returns(mockUserCourseRepository.Object);
        mockUnitOfWork.Setup(r => r.UserMaterialRepository).Returns(mockUserMaterialRepository.Object);
        mockUnitOfWork.Setup(r => r.UserSkillRepository).Returns(mockUserSkillRepository.Object);
        mockUnitOfWork.Setup(r => r.SkillRepository).Returns(mockSkillRepository.Object);
        mockUnitOfWork.Setup(r => r.MaterialRepository).Returns(mockMaterialRepository.Object);
        mockUnitOfWork.Setup(u => u.SaveChangesAsync()).Returns(Task.CompletedTask);

        return mockUnitOfWork;
    }
}