using AutoMapper;
using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Application.Dtos;
using EducationPortal.Application.Services.Interfaces;
using EducationPortal.Application.Exceptions;
using EducationPortal.Data.Entities;

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

    public async Task<ICollection<MaterialDto>> GetMaterialsByCourseIdAsync(int courseId)
    {
        var materials = await _materialRepository.GetMaterialsByCourseIdAsync(courseId);

        return _mapper.Map<List<MaterialDto>>(materials);
    }

    public async Task<MaterialDto> GetByIdAsync(int id)
    {
        var material = await _materialRepository.GetByIdAsync(id);
        if (material == null)
            throw new NotFoundException(nameof(Material), id);

        return _mapper.Map<MaterialDto>(material);
    }

    public async Task<VideoDto> GetVideoByMaterialIdAsync(int materialId)
    {
        var video = await _materialRepository.GetVideoByMaterialIdAsync(materialId);
        if (video == null)
            throw new NotFoundException(nameof(Material), materialId);

        return _mapper.Map<VideoDto>(video);
    }

    public async Task<PublicationDto> GetPublicationByMaterialIdAsync(int materialId)
    {
        var publication = await _materialRepository.GetPublicationByMaterialIdAsync(materialId);
        if (publication == null)
            throw new NotFoundException(nameof(Material), materialId);

        return _mapper.Map<PublicationDto>(publication);
    }

    public async Task<ArticleDto> GetArticleByMaterialIdAsync(int materialId)
    {
        var article = await _materialRepository.GetArticleByMaterialIdAsync(materialId);
        if (article == null)
            throw new NotFoundException(nameof(Material), materialId);

        return _mapper.Map<ArticleDto>(article);
    }
}
