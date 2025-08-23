using AutoMapper;
using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Application.Dtos;
using EducationPortal.Application.Services.Interfaces;

namespace EducationPortal.Application.Services;

public class MaterialService : IMaterialService
{
    private readonly IMaterialRepository _materialRepository;
    private readonly IMapper _mapper;

    public MaterialService(IMaterialRepository materialRepository, IMapper mapper)
    {
        _materialRepository = materialRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<MaterialDto>> GetAllMaterialsAsync()
    {
        var materials = await _materialRepository.GetAllAsync();

        return _mapper.Map<List<MaterialDto>>(materials);
    }

    public async Task<ICollection<MaterialDto>> GetMaterialsByCourseIdAsync(int courseId)
    {
        var materials = await _materialRepository.GetMaterialsByCourseIdAsync(courseId);

        return _mapper.Map<List<MaterialDto>>(materials);
    }

    public async Task<ICollection<VideoDto>> GetVideosByCourseIdAsync(int courseId)
    {
        var videos = await _materialRepository.GetVideosByCourseIdAsync(courseId);

        return _mapper.Map<List<VideoDto>>(videos);
    }

    public async Task<ICollection<PublicationDto>> GetPublicationsByCourseIdAsync(int courseId)
    {
        var publications = await _materialRepository.GetPublicationsByCourseIdAsync(courseId);

        return _mapper.Map<List<PublicationDto>>(publications);
    }

    public async Task<ICollection<ArticleDto>> GetArticlesByCourseIdAsync(int courseId)
    {
        var articles = await _materialRepository.GetArticlesByCourseIdAsync(courseId);

        return _mapper.Map<List<ArticleDto>>(articles);
    }
}
