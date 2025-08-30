using EducationPortal.Application.Dtos;

namespace EducationPortal.Application.Services.Interfaces;

public interface IUserService
{
    Task<ICollection<UserSkillDto>> GetAcquiredSkillsByUserIdAsync(Guid userId);
    Task<ICollection<VideoDto>> GetAcquiredVideosByUserIdAsync(Guid userId);
    Task<ICollection<PublicationDto>> GetAcquiredPublicationsByUserIdAsync(Guid userId);
    Task<ICollection<ArticleDto>> GetAcquiredArticlesByUserIdAsync(Guid userId);
}