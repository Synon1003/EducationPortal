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
using EducationPortal.Data.Entities;
using System.Linq.Expressions;

namespace EducationPortal.Tests.UnitTests;

public class CourseServiceTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly ILogger<CourseService> _logger;
    private readonly Mock<ILogger<CourseService>> _mockLogger;

    private readonly Guid _userId;
    private readonly Course _course;
    private readonly CourseListDto _courseListDto;
    private readonly CourseDetailDto _courseDetailDto;

    private readonly IMapper _mapper;

    public CourseServiceTests()
    {
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<CourseProfile>();
            c.AddProfile<UserCourseProfile>();
            c.AddProfile<SkillProfile>();
            c.AddProfile<MaterialProfile>();
            c.AddProfile<VideoProfile>();
            c.AddProfile<PublicationProfile>();
            c.AddProfile<ArticleProfile>();
        });
        _mapper = mapperConfig.CreateMapper();

        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
        _mockLogger = new Mock<ILogger<CourseService>>();

        _unitOfWork = _mockUnitOfWork.Object;
        _logger = _mockLogger.Object;

        _userId = new Guid();
        _course = new Course
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
        };

        _courseListDto = new CourseListDto
        (
            Id: 1,
            Name: "TestCourseName1",
            Description: "TestCourseDescription1",
            Skills: new List<SkillDto> {
                new SkillDto(1, "Skill1"),
                new SkillDto(2, "Skill2")
            },
            CreatedBy: string.Empty
        );

        _courseDetailDto = new CourseDetailDto
        (
            Id: 2,
            Name: "TestCourseName2",
            Description: "TestCourseDescription2",
            Skills: new List<SkillDto> {
                new SkillDto(1, "Skill1"),
                new SkillDto(2, "Skill2")
            },
            Materials: new List<MaterialDto>
            {
                new MaterialDto(1, "Material1", "Video"),
                new MaterialDto(2, "Material2", "Article"),
            },
            CreatedBy: string.Empty
        );

    }

    [Theory]
    [InlineData("available", nameof(ICourseRepository.GetAvailableCoursesWithSkillsForUserAsync))]
    [InlineData("inprogress", nameof(ICourseRepository.GetInProgressCoursesWithSkillsForUserAsync))]
    [InlineData("completed", nameof(ICourseRepository.GetCompletedCoursesWithSkillsForUserAsync))]
    [InlineData("created", nameof(ICourseRepository.GetCreatedCoursesWithSkillsForUserAsync))]
    [InlineData("all/fallback", nameof(ICourseRepository.GetAllCoursesWithSkillsAsync))]
    public async Task GetFilteredCoursesWithSkillsAsync_WithFilter_CallsCorrectRepositoryMethod(string filter, string expectedMethodName)
    {
        // Arrange
        var service = new CourseService(_unitOfWork, _mapper, _logger);

        // Act
        var result = await service.GetFilteredCoursesWithSkillsAsync(_userId, filter);

        // Assert
        switch (expectedMethodName)
        {
            case nameof(ICourseRepository.GetAvailableCoursesWithSkillsForUserAsync):
                _mockUnitOfWork.Verify(u => u.CourseRepository.GetAvailableCoursesWithSkillsForUserAsync(_userId), Times.Once);
                break;

            case nameof(ICourseRepository.GetInProgressCoursesWithSkillsForUserAsync):
                _mockUnitOfWork.Verify(u => u.CourseRepository.GetInProgressCoursesWithSkillsForUserAsync(_userId), Times.Once);
                break;

            case nameof(ICourseRepository.GetCompletedCoursesWithSkillsForUserAsync):
                _mockUnitOfWork.Verify(u => u.CourseRepository.GetCompletedCoursesWithSkillsForUserAsync(_userId), Times.Once);
                break;

            case nameof(ICourseRepository.GetCreatedCoursesWithSkillsForUserAsync):
                _mockUnitOfWork.Verify(u => u.CourseRepository.GetCreatedCoursesWithSkillsForUserAsync(_userId), Times.Once);
                break;

            case nameof(ICourseRepository.GetAllCoursesWithSkillsAsync):
                _mockUnitOfWork.Verify(u => u.CourseRepository.GetAllCoursesWithSkillsAsync(), Times.Once);
                break;
        }

        result.Should().BeOfType<List<CourseListDto>>();
        result.Should().ContainEquivalentOf(_courseListDto);
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
        _mockUnitOfWork.Verify(u => u.CourseRepository.GetByIdAsync(_courseListDto.Id), Times.Once);
    }

    [Fact]
    public async Task GetCourseByIdAsync_WithCourseDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        int notExistingCourseId = 0;
        CourseService service = new CourseService(_unitOfWork, _mapper, _logger);

        // Act
        Func<Task> act = async () => await service.GetCourseByIdAsync(notExistingCourseId);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Course ({notExistingCourseId}) was not found.");
        _mockUnitOfWork.Verify(u => u.CourseRepository.GetByIdAsync(notExistingCourseId), Times.Once);
    }

    [Fact]
    public async Task GetCourseWithRelationshipsByIdAsync_WithExistingCourse_ReturnsDto()
    {
        // Arrange
        CourseService service = new CourseService(_unitOfWork, _mapper, _logger);

        // Act
        var result = await service.GetCourseWithRelationshipsByIdAsync(_courseDetailDto.Id);

        // Assert
        result.Should().BeOfType<CourseDetailDto>();
        result.Should().BeEquivalentTo(_courseDetailDto);
        _mockUnitOfWork.Verify(u => u.CourseRepository.GetCourseWithRelationshipsByIdAsync(_courseDetailDto.Id), Times.Once);
    }

    [Fact]
    public async Task GetCourseWithRelationshipsByIdAsync_WithNotExistingCourse_ThrowsNotFoundException()
    {
        // Arrange
        int notExistingCourseId = 0;
        CourseService service = new CourseService(_unitOfWork, _mapper, _logger);

        // Act
        Func<Task> act = async () => await service.GetCourseWithRelationshipsByIdAsync(notExistingCourseId);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Course ({notExistingCourseId}) was not found.");
        _mockUnitOfWork.Verify(u => u.CourseRepository.GetCourseWithRelationshipsByIdAsync(notExistingCourseId), Times.Once);
    }

    [Fact]
    public async Task CreateCourseAsync_WithValidCourseCreateDto_InsertsCourseAndReturnsDetailDto()
    {
        // Arrange
        var courseDto = new CourseCreateDto(
            Name: "TestCourse",
            Description: "TestDescription",
            Skills: new List<SkillCreateDto> { new("Skill") },
            LoadedSkills: new List<SkillDto> { new(1, "LoadedSkill") },
            Videos: new List<VideoCreateDto> { new("Video", 60, "HD") },
            Publications: new List<PublicationCreateDto> { new("Publication", "Authors", 10, "pfd", 2025) },
            Articles: new List<ArticleCreateDto> { new("Article", new DateOnly(), "link") },
            LoadedVideos: new List<VideoDto> { new(1, "LoadedVideo", 0, string.Empty) },
            LoadedPublications: new List<PublicationDto> { new(2, "LoadedPublication", "Authors", 10, "pdf", 2025) },
            LoadedArticles: new List<ArticleDto> { new(3, "LoadedArticle", new DateOnly(), "link") },
            CreatedBy: Guid.NewGuid()
        );

        CourseService service = new CourseService(_unitOfWork, _mapper, _logger);

        // Act
        var result = await service.CreateCourseAsync(courseDto);

        // Assert
        _mockUnitOfWork.Verify(r => r.CourseRepository.Insert(It.IsAny<Course>()), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);

        result.Should().NotBeNull();
        result.Name.Should().Be(courseDto.Name);
        result.Description.Should().Be(courseDto.Description);
        result.CreatedBy.Should().Be(string.Empty);

        result.Skills.Should().ContainSingle(s => s.Name == "Skill");
        result.Skills.Should().ContainSingle(s => s.Name == "LoadedSkill");

        result.Materials.Should().ContainSingle(m => m.Title == "Video");
        result.Materials.Should().ContainSingle(m => m.Title == "LoadedVideo");

        result.Materials.Should().ContainSingle(m => m.Title == "Publication");
        result.Materials.Should().ContainSingle(m => m.Title == "LoadedPublication");

        result.Materials.Should().ContainSingle(m => m.Title == "Article");
        result.Materials.Should().ContainSingle(m => m.Title == "LoadedArticle");

        _mockLogger.Verify(
            l => l.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((state, _) => state.ToString()!.Contains(courseDto.Name)),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
            Times.Once
        );
    }

    [Fact]
    public async Task GetUserCourseAsync_WithCorrectFilters_CallsRepositoryMethod()
    {
        // Arrange
        var service = new CourseService(_unitOfWork, _mapper, _logger);

        // Act
        var result = await service.GetUserCourseAsync(_userId, _courseListDto.Id);

        // Assert
        result.Should().NotBeNull();
        result!.CourseId.Should().Be(_courseListDto.Id);
        result.UserId.Should().Be(_userId);

        _mockUnitOfWork.Verify(u =>
            u.UserCourseRepository.GetByFilterAsync(It.Is<Expression<Func<UserCourse, bool>>>(
                expr => expr.Compile().Invoke(new UserCourse { UserId = _userId, CourseId = _courseListDto.Id })
            )
        ),
        Times.Once);
    }

    [Fact]
    public async Task EnrollUserOnCourseAsync_WithCourseNotFound_ThrowsNotFoundException()
    {
        // Arrange
        int notExistingCourseId = 0;
        var service = new CourseService(_unitOfWork, _mapper, _logger);

        // Act
        Func<Task> act = async () => await service.EnrollUserOnCourseAsync(new Guid(), notExistingCourseId);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Course ({notExistingCourseId}) was not found.");
    }

    [Fact]
    public async Task EnrollUserOnCourseAsync_WithNotInstantCompleted_ReturnsFalse()
    {
        // Arrange
        var service = new CourseService(_unitOfWork, _mapper, _logger);

        // Act
        var result = await service.EnrollUserOnCourseAsync(_userId, _courseDetailDto.Id);

        // Assert
        result.Should().BeFalse();
        _mockUnitOfWork.Verify(u => u.UserCourseRepository.Insert(
            It.Is<UserCourse>(uc => uc.UserId == _userId &&
                uc.CourseId == _courseDetailDto.Id && uc.ProgressPercentage == 50 // course2 has 2materials / 1coursematerial
        )), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);

        _mockLogger.Verify(
            l => l.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) =>
                    v.ToString()!.Contains($"<User Id={_userId}> enrolled on <Course Id={_courseDetailDto.Id} Name={_courseDetailDto.Name}>")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);

    }

    [Fact]
    public async Task EnrollUserOnCourseAsync_WithInstantCompleted_ReturnsTrue()
    {
        // Arrange
        var service = new CourseService(_unitOfWork, _mapper, _logger);

        // Act
        var result = await service.EnrollUserOnCourseAsync(_userId, _courseListDto.Id);

        // Assert
        result.Should().BeTrue();
        _mockUnitOfWork.Verify(u => u.UserCourseRepository.Insert(
            It.Is<UserCourse>(uc => uc.UserId == _userId &&
                uc.CourseId == _courseListDto.Id && uc.ProgressPercentage == 100 // course1 has 1material / 1coursematerial
        )), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);

        _mockLogger.Verify(
            l => l.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) =>
                    v.ToString()!.Contains($"<User Id={_userId}> enrolled on <Course Id={_courseListDto.Id} Name={_courseListDto.Name}>")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task LeaveCourseAsync_WithExistingUserCourse_CallsDelete()
    {
        // Arrange
        var service = new CourseService(_mockUnitOfWork.Object, _mapper, _logger);

        // Act
        await service.LeaveCourseAsync(_userId, _courseListDto.Id);

        // Assert
        _mockUnitOfWork.Verify(u => u.UserCourseRepository.Delete(It.IsAny<UserCourse>()), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task LeaveCourseAsync_WithNotExistingUserCourse_ThrowsNotFoundException()
    {
        // Arrange
        int notExistingCourseId = 0;
        var service = new CourseService(_mockUnitOfWork.Object, _mapper, _logger);

        // Act
        Func<Task> act = async () => await service.LeaveCourseAsync(_userId, notExistingCourseId);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"UserCourse (({_userId}, {notExistingCourseId})) was not found.");
    }

    [Theory]
    [InlineData(true, 1)]
    [InlineData(false, 0)] // notExistingMaterialId
    public async Task IsUserDoneWithMaterialAsync_WithExistsAsyncFilter_ReturnsExpected(bool exists, int materialId)
    {
        // Arrange
        var service = new CourseService(_mockUnitOfWork.Object, _mapper, _logger);

        // Act
        var result = await service.IsUserDoneWithMaterialAsync(_userId, materialId);

        // Assert
        result.Should().Be(exists);
        _mockUnitOfWork.Verify(r => r.UserMaterialRepository.ExistsAsync(It.IsAny<Expression<Func<UserMaterial, bool>>>()), Times.Once);
    }

    [Fact]
    public async Task MarkMaterialDoneAsync_WithLastMaterial_CallsUserMaterialInsert()
    {
        // Arrange
        int materialId = _course.Materials.First().Id;
        var service = new CourseService(_mockUnitOfWork.Object, _mapper, _logger);

        // Act
        await service.MarkMaterialDoneAsync(_userId, materialId);

        // Assert
        _mockUnitOfWork.Verify(u => u.UserMaterialRepository.Insert(
            It.Is<UserMaterial>(um => um.UserId == _userId && um.MaterialId == materialId)), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);

        _mockLogger.Verify(
            l => l.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) =>
                    v.ToString()!.Contains($"<User Id={_userId}> marked <Material Id={materialId}> done")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }
}
