using Moq;
using Xunit;
using FluentAssertions;
using AutoMapper;

using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Application.Services;
using EducationPortal.Application.Dtos;
using Microsoft.Extensions.Logging;
using EducationPortal.Tests.Mocks;
using EducationPortal.Application.Mappings;
using EducationPortal.Application.Exceptions;

namespace EducationPortal.Tests;

public class CourseServiceTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly ILogger<CourseService> _logger;
    private readonly Mock<ILogger<CourseService>> _mockLogger;

    private readonly CourseListDto _courseListDto;

    private readonly IMapper _mapper;

    public CourseServiceTests()
    {
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<CourseProfile>();
            c.AddProfile<SkillProfile>();
        });
        _mapper = mapperConfig.CreateMapper();

        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
        _mockLogger = new Mock<ILogger<CourseService>>();

        _unitOfWork = _mockUnitOfWork.Object;
        _logger = _mockLogger.Object;

        _courseListDto = new CourseListDto
        (
            Id: 1,
            Name: "TestCourseName",
            Description: "TestCourseDescription",
            Skills: new List<SkillDto> {
                new SkillDto(1, "Skill1"),
                new SkillDto(2, "Skill2")
            },
            CreatedBy: ""
        );
    }

    [Fact]
    public async Task GetCourseByIdAsync_WithExistingCourse_ReturnsDto()
    {
        // Arrange
        CourseService service = new CourseService(_unitOfWork, _mapper, _logger);

        // Act
        var result = await service.GetCourseByIdAsync(_courseListDto.Id);

        // Assert
        result.Should().BeOfType<CourseListDto>();
        result.Should().BeEquivalentTo(_courseListDto);
    }

    [Fact]
    public async Task GetAsync_WithCourseDoesNotExist_ReturnsNull()
    {
        // Arrange
        int notExistingCourseId = 0;
        CourseService service = new CourseService(_unitOfWork, _mapper, _logger);

        // Act
        Func<Task> act = async () => await service.GetCourseByIdAsync(notExistingCourseId);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Course ({notExistingCourseId}) was not found.");
    }

}
