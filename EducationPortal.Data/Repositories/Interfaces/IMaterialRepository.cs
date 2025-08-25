using EducationPortal.Data.Entities;

namespace EducationPortal.Data.Repositories.Interfaces;

public interface IMaterialRepository : IRepository<Material>
{
    Task<ICollection<Material>> GetMaterialsByCourseIdAsync(int courseId);

    Task<ICollection<Video>> GetAllVideosAsync();
    Task<ICollection<Publication>> GetAllPublicationsAsync();
    Task<ICollection<Article>> GetAllArticlesAsync();

    Task<Video?> GetVideoByMaterialIdAsync(int materialId);
    Task<Publication?> GetPublicationByMaterialIdAsync(int materialId);
    Task<Article?> GetArticleByMaterialIdAsync(int materialId);
}