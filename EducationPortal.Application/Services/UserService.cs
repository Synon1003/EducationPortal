using AutoMapper;
using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Application.Dtos;
using EducationPortal.Application.Services.Interfaces;
using EducationPortal.Data.Entities;
using EducationPortal.Application.Exceptions;

namespace EducationPortal.Application.Services;

public class UserService : IUserService
{
    private readonly IUserSkillRepository _userSkillRepository;
    private readonly ISkillRepository _skillRepository;
    private readonly IMaterialRepository _materialRepository;
    private readonly IMapper _mapper;

    public UserService(
        IUserSkillRepository userSkillRepository,
        ISkillRepository skillRepository,
        IMaterialRepository materialRepository,
        IMapper mapper)
    {
        _userSkillRepository = userSkillRepository;
        _skillRepository = skillRepository;
        _materialRepository = materialRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<SkillDetailDto>> GetAllSkillsAsync()
    {
        var skills = await _skillRepository.GetAllSkillsAsync();
        return _mapper.Map<List<SkillDetailDto>>(skills);
    }

    public async Task<SkillDto> GetSkillByIdAsync(int skillId)
    {
        var skill = await _skillRepository.GetByIdAsync(skillId);
        if (skill == null)
            throw new NotFoundException(nameof(Skill), skillId);

        return _mapper.Map<SkillDto>(skill);
    }

    public async Task<ICollection<VideoDto>> GetVideosCreatedByUserIdAsync(Guid userId)
    {
        var videos = await _materialRepository.GetVideosCreatedByUserIdAsync(userId);
        return _mapper.Map<List<VideoDto>>(videos);
    }

    public async Task<ICollection<PublicationDto>> GetPublicationsCreatedByUserIdAsync(Guid userId)
    {
        var publications = await _materialRepository.GetPublicationsCreatedByUserIdAsync(userId);
        return _mapper.Map<List<PublicationDto>>(publications);
    }

    public async Task<ICollection<ArticleDto>> GetArticlesCreatedByUserIdAsync(Guid userId)
    {
        var articles = await _materialRepository.GetArticlesCreatedByUserIdAsync(userId);
        return _mapper.Map<List<ArticleDto>>(articles);
    }

    public async Task<ICollection<UserSkillDto>> GetAcquiredSkillsByUserIdAsync(Guid userId)
    {
        var userSkills = await _userSkillRepository.GetAllByUserIdAsync(userId);

        return _mapper.Map<List<UserSkillDto>>(userSkills);
    }

    public async Task<ICollection<VideoDto>> GetAcquiredVideosByUserIdAsync(Guid userId)
    {
        var videos = await _materialRepository.GetAcquiredVideosByUserIdAsync(userId);

        return _mapper.Map<List<VideoDto>>(videos);
    }
    public async Task<ICollection<PublicationDto>> GetAcquiredPublicationsByUserIdAsync(Guid userId)
    {
        var publications = await _materialRepository.GetAcquiredPublicationsByUserIdAsync(userId);

        return _mapper.Map<List<PublicationDto>>(publications);
    }
    public async Task<ICollection<ArticleDto>> GetAcquiredArticlesByUserIdAsync(Guid userId)
    {
        var articles = await _materialRepository.GetAcquiredArticlesByUserIdAsync(userId);

        return _mapper.Map<List<ArticleDto>>(articles);
    }
}
