using EducationPortal.Data.Entities;

namespace EducationPortal.Data.Repositories.Interfaces;

public interface IMaterialRepository : IRepository<Material>
{
    Task<ICollection<Video>> GetVideosAsync();
    Task<ICollection<Publication>> GetPublicationsAsync();
    Task<ICollection<Article>> GetArticlesAsync();
}