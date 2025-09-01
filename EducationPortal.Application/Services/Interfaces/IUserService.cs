using EducationPortal.Application.Dtos;

namespace EducationPortal.Application.Services.Interfaces;

public interface IUserService
{
    Task<ICollection<SkillDetailDto>> GetAllSkillsAsync();
    Task<SkillDto> GetSkillByIdAsync(int skillId);
    Task<ICollection<VideoDto>> GetVideosCreatedByUserIdAsync(Guid userId);
    Task<ICollection<PublicationDto>> GetPublicationsCreatedByUserIdAsync(Guid userId);
    Task<ICollection<ArticleDto>> GetArticlesCreatedByUserIdAsync(Guid userId);
    Task<ICollection<UserSkillDto>> GetAcquiredSkillsByUserIdAsync(Guid userId);
    Task<ICollection<VideoDto>> GetAcquiredVideosByUserIdAsync(Guid userId);
    Task<ICollection<PublicationDto>> GetAcquiredPublicationsByUserIdAsync(Guid userId);
    Task<ICollection<ArticleDto>> GetAcquiredArticlesByUserIdAsync(Guid userId);
}