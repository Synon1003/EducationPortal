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

namespace EducationPortal.Tests.UnitTests;

public class GetCourseCreateValidationErrorsAsyncTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly ILogger<CourseService> _logger;
    private readonly Mock<ILogger<CourseService>> _mockLogger;

    private readonly IMapper _mapper;

    public GetCourseCreateValidationErrorsAsyncTests()
    {
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<CourseProfile>();
            c.AddProfile<SkillProfile>();
            c.AddProfile<MaterialProfile>();
        });
        _mapper = mapperConfig.CreateMapper();

        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
        _mockLogger = new Mock<ILogger<CourseService>>();

        _unitOfWork = _mockUnitOfWork.Object;
        _logger = _mockLogger.Object;
    }

    public class GetValidationErrorsTestCase
    {
        public string CaseName { get; set; } = string.Empty;
        public CourseCreateDto courseCreateDto { get; set; } = default!;
        public string ExpectedError { get; set; } = string.Empty;
    }

    public static IEnumerable<object[]> GetValidationErrorsTestCases =>
    new List<object[]>
    {
        new object[]
        {
            new GetValidationErrorsTestCase
            {
                CaseName = "Course already taken",
                courseCreateDto = new CourseCreateDto(
                    "ExistingCourseName",
                    "ExistingCourseDescription",
                    [], [], [], [], [], [], [], [], Guid.NewGuid()
                ),
                ExpectedError = "CourseName(ExistingCourseName)IsAlreadyTaken"
            }
        },
        new object[]
        {
            new GetValidationErrorsTestCase
            {
                CaseName = "Skill already taken",
                courseCreateDto = new CourseCreateDto(
                    "UniqueCourseName",
                    "UniqueCourseDescription",
                    [ new SkillCreateDto("ExistingSkillName") ],
                    [], [], [], [], [], [], [], Guid.NewGuid()
                ),
                ExpectedError = "SkillName(ExistingSkillName)IsAlreadyTaken"
            }
        },
        new object[]
        {
            new GetValidationErrorsTestCase
            {
                CaseName = "Skill duplicated",
                courseCreateDto = new CourseCreateDto(
                    "UniqueCourseName",
                    "UniqueCourseDescription",
                    [
                        new SkillCreateDto("DuplicatedSkillName"),
                        new SkillCreateDto("DuplicatedSkillName")
                    ],
                    [], [], [], [], [], [], [], Guid.NewGuid()
                ),
                ExpectedError = "SkillName(DuplicatedSkillName)IsDuplicated"
            }
        },
        new object[]
        {
            new GetValidationErrorsTestCase
            {
                CaseName = "Video already taken",
                courseCreateDto = new CourseCreateDto(
                    "UniqueCourseName",
                    "UniqueCourseDescription",
                    [], [ new VideoCreateDto("ExistingVideoTitle", 0, string.Empty) ],
                    [], [], [], [], [], [], Guid.NewGuid()
                ),
                ExpectedError = "VideoTitle(ExistingVideoTitle)IsAlreadyTaken"
            }
        },
        new object[]
        {
            new GetValidationErrorsTestCase
            {
                CaseName = "Video duplicated",
                courseCreateDto = new CourseCreateDto(
                    "UniqueCourseName",
                    "UniqueCourseDescription",
                    [], [
                        new VideoCreateDto("DuplicatedVideoTitle", 0, string.Empty),
                        new VideoCreateDto("DuplicatedVideoTitle", 0, string.Empty)
                    ], [], [], [], [], [], [], Guid.NewGuid()
                ),
                ExpectedError = "VideoTitle(DuplicatedVideoTitle)IsDuplicated"
            }
        },
        new object[]
        {
            new GetValidationErrorsTestCase
            {
                CaseName = "Publication already taken",
                courseCreateDto = new CourseCreateDto(
                    "UniqueCourseName",
                    "UniqueCourseDescription",
                    [], [],
                    [ new PublicationCreateDto("ExistingPublicationTitle", string.Empty, 0, string.Empty, 0) ],
                    [], [], [], [], [], Guid.NewGuid()
                ),
                ExpectedError = "PublicationTitle(ExistingPublicationTitle)IsAlreadyTaken"
            }
        },
        new object[]
        {
            new GetValidationErrorsTestCase
            {
                CaseName = "Publication duplicated",
                courseCreateDto = new CourseCreateDto(
                    "UniqueCourseName",
                    "UniqueCourseDescription",
                    [], [], [
                        new PublicationCreateDto("DuplicatedPublicationTitle", string.Empty, 0, string.Empty, 0),
                        new PublicationCreateDto("DuplicatedPublicationTitle", string.Empty, 0, string.Empty, 0)
                    ], [], [], [], [], [], Guid.NewGuid()
                ),
                ExpectedError = "PublicationTitle(DuplicatedPublicationTitle)IsDuplicated"
            }
        },
        new object[]
        {
            new GetValidationErrorsTestCase
            {
                CaseName = "Article already taken",
                courseCreateDto = new CourseCreateDto(
                    "UniqueCourseName",
                    "UniqueCourseDescription",
                    [], [], [], [ new ArticleCreateDto("ExistingArticleTitle", new DateOnly(), string.Empty) ],
                    [], [], [], [], Guid.NewGuid()
                ),
                ExpectedError = "ArticleTitle(ExistingArticleTitle)IsAlreadyTaken"
            }
        },
        new object[]
        {
            new GetValidationErrorsTestCase
            {
                CaseName = "Article duplicated",
                courseCreateDto = new CourseCreateDto(
                    "UniqueCourseName",
                    "UniqueCourseDescription",
                    [], [], [], [
                        new ArticleCreateDto("DuplicatedArticleTitle", new DateOnly(), string.Empty),
                        new ArticleCreateDto("DuplicatedArticleTitle", new DateOnly(), string.Empty)
                    ], [], [], [], [], Guid.NewGuid()
                ),
                ExpectedError = "ArticleTitle(DuplicatedArticleTitle)IsDuplicated"
            }
        }
    };

    [Theory]
    [MemberData(nameof(GetValidationErrorsTestCases))]
    public async Task GetCourseCreateValidationErrorsAsync_WithExistingProperty_AddsValidationError(GetValidationErrorsTestCase testCase)
    {
        // Arrange
        CourseService service = new CourseService(_unitOfWork, _mapper, _logger);

        // Act
        List<string> result = await service.GetCourseCreateValidationErrorsAsync(testCase.courseCreateDto);

        // Assert
        result.Should().ContainSingle().Which.Should().Be(testCase.ExpectedError);
    }

    [Fact]
    public async Task GetCourseCreateValidationErrorsAsync_WithUniqueProperty_NoValidationError()
    {
        // Arrange
        CourseCreateDto _courseCreateDto = new CourseCreateDto
        (
            Name: "UniqueCourseName",
            Description: "UniqueCourseDescription",
            Skills: [new SkillCreateDto("UniqueSkillName")],
            Videos: [new VideoCreateDto("UniqueVideoTitle", 0, string.Empty)],
            Publications: [new PublicationCreateDto("UniquePublicationTitle", string.Empty, 0, string.Empty, 0)],
            Articles: [new ArticleCreateDto("UniqueArticleTitle", new DateOnly(), string.Empty)],
            LoadedSkills: [], LoadedVideos: [], LoadedPublications: [],
            LoadedArticles: [], CreatedBy: new Guid()
        );
        CourseService service = new CourseService(_unitOfWork, _mapper, _logger);

        // Act
        List<string> result = await service.GetCourseCreateValidationErrorsAsync(_courseCreateDto);

        // Assert
        result.Should().BeEmpty();
    }
}
