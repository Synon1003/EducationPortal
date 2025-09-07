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

public class MaterialServiceTests
{
    private readonly IMaterialRepository _materialRepository;
    private readonly Mock<IMaterialRepository> _mockMaterialRepository;

    private readonly int _courseId;
    private readonly int _materialId;

    private readonly IMapper _mapper;

    public MaterialServiceTests()
    {
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<MaterialProfile>();
            c.AddProfile<VideoProfile>();
            c.AddProfile<PublicationProfile>();
            c.AddProfile<ArticleProfile>();
        });
        _mapper = mapperConfig.CreateMapper();

        _mockMaterialRepository = new Mock<IMaterialRepository>();
        _materialRepository = _mockMaterialRepository.Object;

        _courseId = 1;
        _materialId = 1;
    }

    [Fact]
    public async Task GetMaterialsByCourseIdAsync_ReturnsDetailDtos()
    {
        // Arrange
        MaterialService service = new MaterialService(
            _materialRepository,
            _mapper);

        List<MaterialDto> materialDtos = [new MaterialDto(1, "Material1", "Video")];

        _mockMaterialRepository.Setup(r => r.GetMaterialsByCourseIdAsync(_courseId))
            .ReturnsAsync([new Material { Id = 1, Title = "Material1", Type = "Video" }]);

        // Act
        var result = await service.GetMaterialsByCourseIdAsync(_courseId);

        // Assert
        result.Should().BeOfType<List<MaterialDto>>();
        result.Should().BeEquivalentTo(materialDtos);
        _mockMaterialRepository.Verify(r => r.GetMaterialsByCourseIdAsync(_courseId), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithExistingMaterial_ReturnsDto()
    {
        // Arrange
        MaterialService service = new MaterialService(
            _materialRepository,
            _mapper);

        MaterialDto materialDto = new MaterialDto(1, "Material1", "Video");

        _mockMaterialRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Material { Id = 1, Title = "Material1", Type = "Video" });

        // Act
        var result = await service.GetByIdAsync(materialDto.Id);

        // Assert
        result.Should().BeOfType<MaterialDto>();
        result.Should().BeEquivalentTo(materialDto);
        _mockMaterialRepository.Verify(r => r.GetByIdAsync(materialDto.Id), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithMaterialDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        int notExistingMaterialId = 0;
        MaterialService service = new MaterialService(
            _materialRepository,
            _mapper);

        _mockMaterialRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => null);

        // Act
        Func<Task> act = async () => await service.GetByIdAsync(notExistingMaterialId);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Material ({notExistingMaterialId}) was not found.");
        _mockMaterialRepository.Verify(r => r.GetByIdAsync(notExistingMaterialId), Times.Once);
    }

    [Fact]
    public async Task GetVideoByMaterialIdAsync_WithExistingVideo_ReturnsDto()
    {
        // Arrange
        VideoDto videoDto = new VideoDto(1, "Video1", 10, "HD");

        _mockMaterialRepository
            .Setup(r => r.GetVideoByMaterialIdAsync(_materialId))
            .ReturnsAsync(new Video
            {
                Id = 1,
                Title = "Video1",
                Duration = 10,
                Quality = "HD"
            });

        MaterialService service = new MaterialService(
            _materialRepository,
            _mapper);

        // Act
        var result = await service.GetVideoByMaterialIdAsync(_materialId);

        // Assert
        _mockMaterialRepository.Verify(r => r.GetVideoByMaterialIdAsync(_materialId), Times.Once);

        result.Should().BeEquivalentTo(videoDto);
    }

    [Fact]
    public async Task GetVideoByMaterialIdAsync_WithVideoDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        int notExistingMaterialId = 0;
        MaterialService service = new MaterialService(
            _materialRepository,
            _mapper);

        _mockMaterialRepository.Setup(r => r.GetVideoByMaterialIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => null);

        // Act
        Func<Task> act = async () => await service.GetVideoByMaterialIdAsync(notExistingMaterialId);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Material ({notExistingMaterialId}) was not found.");
        _mockMaterialRepository.Verify(r => r.GetVideoByMaterialIdAsync(notExistingMaterialId), Times.Once);
    }

    [Fact]
    public async Task GetPublicationByMaterialIdAsync_WithExistingPublication_ReturnsDto()
    {
        // Arrange
        PublicationDto publicationDto = new PublicationDto(1, "Publication1", "Authors", 0, "pdf", 2025);

        _mockMaterialRepository
            .Setup(r => r.GetPublicationByMaterialIdAsync(_materialId))
            .ReturnsAsync(new Publication
            {
                Id = 1,
                Title = "Publication1",
                Authors = "Authors",
                Pages = 0,
                Format = "pdf",
                PublicationYear = 2025
            });

        MaterialService service = new MaterialService(
            _materialRepository,
            _mapper);

        // Act
        var result = await service.GetPublicationByMaterialIdAsync(_materialId);

        // Assert
        _mockMaterialRepository.Verify(r => r.GetPublicationByMaterialIdAsync(_materialId), Times.Once);

        result.Should().BeEquivalentTo(publicationDto);
    }

    [Fact]
    public async Task GetPublicationByMaterialIdAsync_WithPublicationDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        int notExistingMaterialId = 0;
        MaterialService service = new MaterialService(
            _materialRepository,
            _mapper);

        _mockMaterialRepository.Setup(r => r.GetPublicationByMaterialIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => null);

        // Act
        Func<Task> act = async () => await service.GetPublicationByMaterialIdAsync(notExistingMaterialId);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Material ({notExistingMaterialId}) was not found.");
        _mockMaterialRepository.Verify(r => r.GetPublicationByMaterialIdAsync(notExistingMaterialId), Times.Once);
    }

    [Fact]
    public async Task GetArticleByMaterialIdAsync_WithExistingArticle_ReturnsDto()
    {
        // Arrange
        ArticleDto articleDto = new ArticleDto(1, "Article1", new DateOnly(), "link");

        _mockMaterialRepository
            .Setup(r => r.GetArticleByMaterialIdAsync(_materialId))
            .ReturnsAsync(new Article
            {
                Id = 1,
                Title = "Article1",
                PublicationDate = new DateOnly(),
                ResourceLink = "link"
            });

        MaterialService service = new MaterialService(
            _materialRepository,
            _mapper);

        // Act
        var result = await service.GetArticleByMaterialIdAsync(_materialId);

        // Assert
        _mockMaterialRepository.Verify(r => r.GetArticleByMaterialIdAsync(_materialId), Times.Once);

        result.Should().BeEquivalentTo(articleDto);
    }

    [Fact]
    public async Task GetArticleByMaterialIdAsync_WithArticleDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        int notExistingMaterialId = 0;
        MaterialService service = new MaterialService(
            _materialRepository,
            _mapper);

        _mockMaterialRepository.Setup(r => r.GetArticleByMaterialIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => null);

        // Act
        Func<Task> act = async () => await service.GetArticleByMaterialIdAsync(notExistingMaterialId);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Material ({notExistingMaterialId}) was not found.");
        _mockMaterialRepository.Verify(r => r.GetArticleByMaterialIdAsync(notExistingMaterialId), Times.Once);
    }
}
