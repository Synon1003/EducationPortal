using EducationPortal.Application.Dtos;

namespace EducationPortal.Application.Services.Interfaces;

public interface IMaterialService
{
    Task<ICollection<MaterialDto>> GetAllMaterialsAsync();
    Task<ICollection<MaterialDto>> GetMaterialsByCourseIdAsync(int courseId);
    Task<MaterialDto> GetByIdAsync(int id);

    Task<ICollection<VideoDto>> GetVideosWithMaterialByCourseIdAsync(int courseId);
    Task<ICollection<PublicationDto>> GetPublicationsWithMaterialByCourseIdAsync(int courseId);
    Task<ICollection<ArticleDto>> GetArticlesWithMaterialByCourseIdAsync(int courseId);

    Task<VideoDto> GetVideoByMaterialIdAsync(int materialId);
    Task<PublicationDto> GetPublicationByMaterialIdAsync(int materialId);
    Task<ArticleDto> GetArticleByMaterialIdAsync(int materialId);
}