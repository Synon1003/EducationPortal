using AutoMapper;
using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Application.Dtos;
using EducationPortal.Application.Services.Interfaces;

namespace EducationPortal.Application.Services;

public class UserService : IUserService
{
    private readonly IUserSkillRepository _userSkillRepository;
    private readonly IMaterialRepository _materialRepository;

    private readonly IMapper _mapper;

    public UserService(
        IUserSkillRepository userSkillRepository,
        IMaterialRepository materialRepository,
        IMapper mapper)
    {
        _userSkillRepository = userSkillRepository;
        _materialRepository = materialRepository;
        _mapper = mapper;
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
