using EducationPortal.Data.Entities;

namespace EducationPortal.Data.Repositories.Interfaces;

public interface IMaterialRepository : IRepository<Material>
{
    Task<ICollection<Material>> GetMaterialsByCourseIdAsync(int courseId);

    Task<ICollection<Video>> GetAllVideosAsync();
    Task<ICollection<Video>> GetVideosWithMaterialByCourseIdAsync(int courseId);
    Task<Video?> GetVideoByMaterialIdAsync(int materialId);

    Task<ICollection<Publication>> GetAllPublicationsAsync();
    Task<ICollection<Publication>> GetPublicationsWithMaterialByCourseIdAsync(int courseId);
    Task<Publication?> GetPublicationByMaterialIdAsync(int materialId);

    Task<ICollection<Article>> GetAllArticlesAsync();
    Task<ICollection<Article>> GetArticlesWithMaterialByCourseIdAsync(int courseId);
    Task<Article?> GetArticleByMaterialIdAsync(int materialId);
}