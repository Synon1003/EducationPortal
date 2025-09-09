using EducationPortal.Data.Entities;

namespace EducationPortal.Data.Repositories.Interfaces;

public interface IMaterialRepository : IRepository<Material>
{
    Task<ICollection<Material>> GetMaterialsByCourseIdAsync(int courseId);

    Task<ICollection<Video>> GetAcquiredVideosByUserIdAsync(Guid userId);
    Task<ICollection<Publication>> GetAcquiredPublicationsByUserIdAsync(Guid userId);
    Task<ICollection<Article>> GetAcquiredArticlesByUserIdAsync(Guid userId);

    Task<ICollection<Video>> GetVideosCreatedByUserIdAsync(Guid userId);
    Task<ICollection<Publication>> GetPublicationsCreatedByUserIdAsync(Guid userId);
    Task<ICollection<Article>> GetArticlesCreatedByUserIdAsync(Guid userId);

    Task<Video?> GetVideoByMaterialIdAsync(int materialId);
    Task<Publication?> GetPublicationByMaterialIdAsync(int materialId);
    Task<Article?> GetArticleByMaterialIdAsync(int materialId);
}