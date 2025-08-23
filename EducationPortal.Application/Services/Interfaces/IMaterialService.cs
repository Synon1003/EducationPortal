using EducationPortal.Application.Dtos;

namespace EducationPortal.Application.Services.Interfaces;

public interface IMaterialService
{
    Task<ICollection<MaterialDto>> GetAllMaterialsAsync();
    Task<ICollection<MaterialDto>> GetMaterialsByCourseIdAsync(int courseId);

    Task<ICollection<VideoDto>> GetVideosByCourseIdAsync(int courseId);
    Task<ICollection<PublicationDto>> GetPublicationsByCourseIdAsync(int courseId);
    Task<ICollection<ArticleDto>> GetArticlesByCourseIdAsync(int courseId);
}