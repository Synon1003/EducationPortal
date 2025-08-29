using EducationPortal.Application.Dtos;

namespace EducationPortal.Application.Services.Interfaces;

public interface IMaterialService
{
    Task<ICollection<MaterialDto>> GetAllMaterialsAsync();
    Task<ICollection<MaterialDto>> GetMaterialsByCourseIdAsync(int courseId);
    bool IsUserDoneWithMaterial(Guid userId, int materialId);
    Task<bool> MarkMaterialDone(Guid userId, int materialId, int courseId);
    Task<MaterialDto> GetByIdAsync(int id);

    Task<VideoDto> GetVideoByMaterialIdAsync(int materialId);
    Task<PublicationDto> GetPublicationByMaterialIdAsync(int materialId);
    Task<ArticleDto> GetArticleByMaterialIdAsync(int materialId);
}