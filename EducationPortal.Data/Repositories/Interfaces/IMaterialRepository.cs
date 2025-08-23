using EducationPortal.Data.Entities;

namespace EducationPortal.Data.Repositories.Interfaces;

public interface IMaterialRepository : IRepository<Material>
{
    Task<ICollection<Material>> GetMaterialsByCourseIdAsync(int courseId);
    Task<ICollection<Video>> GetAllVideosAsync();
    Task<ICollection<Video>> GetVideosByCourseIdAsync(int courseId);
    Task<ICollection<Publication>> GetAllPublicationsAsync();
    Task<ICollection<Publication>> GetPublicationsByCourseIdAsync(int courseId);
    Task<ICollection<Article>> GetAllArticlesAsync();
    Task<ICollection<Article>> GetArticlesByCourseIdAsync(int courseId);
}