using Moq;

using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Data.Entities;
using System.Linq.Expressions;

namespace EducationPortal.Tests.Mocks;

public static class MockMaterialRepository
{
    public static Mock<IMaterialRepository> GetMaterialRepository()
    {
        var mockRepository = new Mock<IMaterialRepository>();

        var existingMaterials = new List<Material>
        {
            new Video { Title = "ExistingVideoTitle", Type = "Video" },
            new Publication { Title = "ExistingPublicationTitle", Type = "Publication" },
            new Article { Title = "ExistingArticleTitle", Type = "Article" }
        };
        mockRepository.Setup(r => r.ExistsAsync(It.IsAny<Expression<Func<Material, bool>>>()))
            .ReturnsAsync((Expression<Func<Material, bool>> predicate) => existingMaterials.Any(predicate.Compile()));

        mockRepository.Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Material, bool>>>()))
            .ReturnsAsync((Expression<Func<Material, bool>> predicate) => [
                new Video { Id = 1, Title = "LoadedVideo" },
                new Publication { Id = 2, Title = "LoadedPublication" },
                new Article { Id = 3, Title = "LoadedArticle" }
            ]);

        return mockRepository;
    }
}