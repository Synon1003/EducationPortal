using Moq;
using Xunit;
using FluentAssertions;
using AutoMapper;

using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Application.Services;
using EducationPortal.Application.Dtos;
using EducationPortal.Application.Mappings;
using EducationPortal.Application.Exceptions;
using EducationPortal.Data.Entities;

namespace EducationPortal.Tests.UnitTests;

public class UserServiceTests
{
    private readonly IUserSkillRepository _userSkillRepository;
    private readonly Mock<IUserSkillRepository> _mockUserSkillRepository;
    private readonly ISkillRepository _skillRepository;
    private readonly Mock<ISkillRepository> _mockSkillRepository;
    private readonly IMaterialRepository _materialRepository;
    private readonly Mock<IMaterialRepository> _mockMaterialRepository;

    private readonly Guid _userId;

    private readonly IMapper _mapper;

    public UserServiceTests()
    {
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<SkillProfile>();
            c.AddProfile<UserSkillProfile>();
            c.AddProfile<VideoProfile>();
            c.AddProfile<PublicationProfile>();
            c.AddProfile<ArticleProfile>();
        });
        _mapper = mapperConfig.CreateMapper();

        _mockUserSkillRepository = new Mock<IUserSkillRepository>();
        _mockSkillRepository = new Mock<ISkillRepository>();
        _mockMaterialRepository = new Mock<IMaterialRepository>();

        _userSkillRepository = _mockUserSkillRepository.Object;
        _skillRepository = _mockSkillRepository.Object;
        _materialRepository = _mockMaterialRepository.Object;

        _userId = new Guid();
    }

    [Fact]
    public async Task GetAllSkillsAsync_ReturnsDetailDtos()
    {
        // Arrange
        UserService service = new UserService(
            _userSkillRepository,
            _skillRepository,
            _materialRepository,
            _mapper);

        Skill skill = new Skill { Id = 1, Name = "Skill1", UserSkills = [] };
        List<SkillDetailDto> skillDetailDtos = [
            new SkillDetailDto(1, "Skill1", skill.UserSkills.Count, 0)];

        _mockSkillRepository.Setup(r => r.GetAllSkillsAsync())
            .ReturnsAsync([skill]);

        // Act
        var result = await service.GetAllSkillsAsync();

        // Assert
        result.Should().BeOfType<List<SkillDetailDto>>();
        result.Should().BeEquivalentTo(skillDetailDtos);
        _mockSkillRepository.Verify(r => r.GetAllSkillsAsync(), Times.Once);
    }

    [Fact]
    public async Task GetSkillByIdAsync_WithExistingSkill_ReturnsDto()
    {
        // Arrange
        UserService service = new UserService(
            _userSkillRepository,
            _skillRepository,
            _materialRepository,
            _mapper);

        SkillDto skillDto = new SkillDto(Id: 1, Name: "Skill1");

        _mockSkillRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Skill { Id = skillDto.Id, Name = skillDto.Name });

        // Act
        var result = await service.GetSkillByIdAsync(skillDto.Id);

        // Assert
        result.Should().BeOfType<SkillDto>();
        result.Should().BeEquivalentTo(skillDto);
        _mockSkillRepository.Verify(r => r.GetByIdAsync(skillDto.Id), Times.Once);
    }

    [Fact]
    public async Task GetCourseByIdAsync_WithCourseDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        int notExistingSkillId = 0;
        UserService service = new UserService(
            _userSkillRepository,
            _skillRepository,
            _materialRepository,
            _mapper);

        _mockSkillRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => null);

        // Act
        Func<Task> act = async () => await service.GetSkillByIdAsync(notExistingSkillId);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Skill({notExistingSkillId})WasNotFound");
        _mockSkillRepository.Verify(r => r.GetByIdAsync(notExistingSkillId), Times.Once);
    }

    [Fact]
    public async Task GetVideosCreatedByUserIdAsync_WhenVideosExist_ReturnsDtos()
    {
        // Arrange
        List<Video> videos = [new Video {
            Id = 1, Title = "Video1", Duration = 10, Quality = "HD"
        }];

        List<VideoDto> videoDtos = [new VideoDto(1, "Video1", 10, "HD")];

        _mockMaterialRepository
            .Setup(r => r.GetVideosCreatedByUserIdAsync(_userId))
            .ReturnsAsync(videos);

        UserService service = new UserService(
            _userSkillRepository,
            _skillRepository,
            _materialRepository,
            _mapper);

        // Act
        var result = await service.GetVideosCreatedByUserIdAsync(_userId);

        // Assert
        _mockMaterialRepository.Verify(r => r.GetVideosCreatedByUserIdAsync(_userId), Times.Once);

        result.Should().BeEquivalentTo(videoDtos);
    }

    [Fact]
    public async Task GetPublicationsCreatedByUserIdAsync_WhenPublicationsExist_ReturnsDtos()
    {
        // Arrange
        List<Publication> publications = [new Publication {
            Id = 1,
            Title = "Publication1",
            Authors = "Authors",
            Pages = 0,
            Format = "pdf",
            PublicationYear = 2025
        }];

        List<PublicationDto> publicationDtos = [
            new PublicationDto(1, "Publication1", "Authors", 0, "pdf", 2025)
        ];

        _mockMaterialRepository
            .Setup(r => r.GetPublicationsCreatedByUserIdAsync(_userId))
            .ReturnsAsync(publications);

        UserService service = new UserService(
            _userSkillRepository,
            _skillRepository,
            _materialRepository,
            _mapper);

        // Act
        var result = await service.GetPublicationsCreatedByUserIdAsync(_userId);

        // Assert
        _mockMaterialRepository.Verify(r => r.GetPublicationsCreatedByUserIdAsync(_userId), Times.Once);

        result.Should().BeEquivalentTo(publicationDtos);
    }

    [Fact]
    public async Task GetArticlesCreatedByUserIdAsync_WhenArticlesExist_ReturnsDtos()
    {
        // Arrange
        List<Article> articles = [new Article {
            Id = 1, Title = "Article1", PublicationDate = new DateOnly(), ResourceLink = "link"
        }];

        List<ArticleDto> articleDtos = [
            new ArticleDto(1, "Article1", new DateOnly(), "link")
        ];

        _mockMaterialRepository
            .Setup(r => r.GetArticlesCreatedByUserIdAsync(_userId))
            .ReturnsAsync(articles);

        UserService service = new UserService(
            _userSkillRepository,
            _skillRepository,
            _materialRepository,
            _mapper);

        // Act
        var result = await service.GetArticlesCreatedByUserIdAsync(_userId);

        // Assert
        _mockMaterialRepository.Verify(r => r.GetArticlesCreatedByUserIdAsync(_userId), Times.Once);

        result.Should().BeEquivalentTo(articleDtos);
    }

    [Fact]
    public async Task GetAcquiredSkillsByUserIdAsync_WhenSkillsExist_ReturnsDtos()
    {
        // Arrange
        List<UserSkill> userSkills = [
            new UserSkill { SkillId = 1, UserId = _userId, Level = 1,
            Skill = new Skill { Id = 1, Name = "Skill1" } }
        ];
        List<UserSkillDto> userSkillDtos = [
            new UserSkillDto(_userId, 1, "Skill1", 1)
        ];

        _mockUserSkillRepository
            .Setup(r => r.GetAllByUserIdAsync(_userId))
            .ReturnsAsync(userSkills);

        UserService service = new UserService(
            _userSkillRepository,
            _skillRepository,
            _materialRepository,
            _mapper);

        // Act
        var result = await service.GetAcquiredSkillsByUserIdAsync(_userId);

        // Assert
        _mockUserSkillRepository.Verify(r => r.GetAllByUserIdAsync(_userId), Times.Once);

        result.Should().BeEquivalentTo(userSkillDtos);
    }

    [Fact]
    public async Task GetAcquiredVideosByUserIdAsync_WhenVideosExist_ReturnsDtos()
    {
        // Arrange
        List<Video> videos = [new Video {
            Id = 1, Title = "Video1", Duration = 10, Quality = "HD"
        }];

        List<VideoDto> videoDtos = [new VideoDto(1, "Video1", 10, "HD")];

        _mockMaterialRepository
            .Setup(r => r.GetAcquiredVideosByUserIdAsync(_userId))
            .ReturnsAsync(videos);

        UserService service = new UserService(
            _userSkillRepository,
            _skillRepository,
            _materialRepository,
            _mapper);

        // Act
        var result = await service.GetAcquiredVideosByUserIdAsync(_userId);

        // Assert
        _mockMaterialRepository.Verify(r => r.GetAcquiredVideosByUserIdAsync(_userId), Times.Once);

        result.Should().BeEquivalentTo(videoDtos);
    }

    [Fact]
    public async Task GetAcquiredPublicationsByUserIdAsync_WhenPublicationsExist_ReturnsDtos()
    {
        // Arrange
        List<Publication> publications = [new Publication {
            Id = 1,
            Title = "Publication1",
            Authors = "Authors",
            Pages = 0,
            Format = "pdf",
            PublicationYear = 2025
        }];

        List<PublicationDto> publicationDtos = [
            new PublicationDto(1, "Publication1", "Authors", 0, "pdf", 2025)
        ];

        _mockMaterialRepository
            .Setup(r => r.GetAcquiredPublicationsByUserIdAsync(_userId))
            .ReturnsAsync(publications);

        UserService service = new UserService(
            _userSkillRepository,
            _skillRepository,
            _materialRepository,
            _mapper);

        // Act
        var result = await service.GetAcquiredPublicationsByUserIdAsync(_userId);

        // Assert
        _mockMaterialRepository.Verify(r => r.GetAcquiredPublicationsByUserIdAsync(_userId), Times.Once);

        result.Should().BeEquivalentTo(publicationDtos);
    }

    [Fact]
    public async Task GetAcquiredArticlesByUserIdAsync_WhenArticlesExist_ReturnsDtos()
    {
        // Arrange
        List<Article> articles = [new Article {
            Id = 1, Title = "Article1", PublicationDate = new DateOnly(), ResourceLink = "link"
        }];

        List<ArticleDto> articleDtos = [
            new ArticleDto(1, "Article1", new DateOnly(), "link")
        ];

        _mockMaterialRepository
            .Setup(r => r.GetAcquiredArticlesByUserIdAsync(_userId))
            .ReturnsAsync(articles);

        UserService service = new UserService(
            _userSkillRepository,
            _skillRepository,
            _materialRepository,
            _mapper);

        // Act
        var result = await service.GetAcquiredArticlesByUserIdAsync(_userId);

        // Assert
        _mockMaterialRepository.Verify(r => r.GetAcquiredArticlesByUserIdAsync(_userId), Times.Once);

        result.Should().BeEquivalentTo(articleDtos);
    }
}
