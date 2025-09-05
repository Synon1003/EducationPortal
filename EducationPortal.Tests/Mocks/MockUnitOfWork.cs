using Moq;

using EducationPortal.Data.Repositories.Interfaces;

namespace EducationPortal.Tests.Mocks;

public static class MockUnitOfWork
{
    public static Mock<IUnitOfWork> GetUnitOfWork()
    {
        var mockUow = new Mock<IUnitOfWork>();
        var mockCourseRepository = MockCourseRepository.GetCourseRepository();

        mockUow.Setup(r => r.CourseRepository).Returns(mockCourseRepository.Object);

        return mockUow;
    }
}