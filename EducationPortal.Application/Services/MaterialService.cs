using AutoMapper;
using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Application.Dtos;
using EducationPortal.Application.Services.Interfaces;
using EducationPortal.Application.Exceptions;
using EducationPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.Application.Services;

public class MaterialService : IMaterialService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MaterialService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ICollection<MaterialDto>> GetAllMaterialsAsync()
    {
        var materials = await _unitOfWork.MaterialRepository.GetAll().ToListAsync();

        return _mapper.Map<List<MaterialDto>>(materials);
    }

    public async Task<ICollection<MaterialDto>> GetMaterialsByCourseIdAsync(int courseId)
    {
        var materials = await _unitOfWork.MaterialRepository.GetMaterialsByCourseIdAsync(courseId);

        return _mapper.Map<List<MaterialDto>>(materials);
    }

    public bool IsUserDoneWithMaterial(Guid userId, int materialId)
    {
        return _unitOfWork.UserMaterialRepository
            .Exists(c => c.UserId == userId && c.MaterialId == materialId);
    }

    public async Task<bool> MarkMaterialDone(Guid userId, int materialId, int courseId)
    {
        var course = await _unitOfWork.CourseRepository
            .GetCourseWithRelationshipsByIdAsync(courseId);
        if (course is null)
            throw new NotFoundException(nameof(Course), courseId);

        var userCourse = await _unitOfWork.UserCourseRepository
            .GetByFilterAsync(uc => uc.UserId == userId && uc.CourseId == courseId);
        if (userCourse is null)
            throw new NotFoundException(nameof(UserCourse), (userId, courseId));

        int acquiredUserMaterials = CountUserMaterials(userId, course.Materials);
        userCourse.ProgressPercentage = 100 * (acquiredUserMaterials + 1) / course.Materials.Count;

        await _unitOfWork.UserMaterialRepository.InsertAsync(
            new UserMaterial() { UserId = userId, MaterialId = materialId });

        _unitOfWork.UserCourseRepository.Update(userCourse);

        // if (userCourse.IsCompleted) TODO UserSkills
        await _unitOfWork.SaveChangesAsync();

        return userCourse.IsCompleted;
    }

    public async Task<MaterialDto> GetByIdAsync(int id)
    {
        var material = await _unitOfWork.MaterialRepository.GetByIdAsync(id);
        if (material == null)
            throw new NotFoundException(nameof(Material), id);

        return _mapper.Map<MaterialDto>(material);
    }

    public async Task<VideoDto> GetVideoByMaterialIdAsync(int materialId)
    {
        var video = await _unitOfWork.MaterialRepository.GetVideoByMaterialIdAsync(materialId);
        if (video == null)
            throw new NotFoundException(nameof(Material), materialId);

        return _mapper.Map<VideoDto>(video);
    }

    public async Task<PublicationDto> GetPublicationByMaterialIdAsync(int materialId)
    {
        var publication = await _unitOfWork.MaterialRepository.GetPublicationByMaterialIdAsync(materialId);
        if (publication == null)
            throw new NotFoundException(nameof(Material), materialId);

        return _mapper.Map<PublicationDto>(publication);
    }

    public async Task<ArticleDto> GetArticleByMaterialIdAsync(int materialId)
    {
        var article = await _unitOfWork.MaterialRepository.GetArticleByMaterialIdAsync(materialId);
        if (article == null)
            throw new NotFoundException(nameof(Material), materialId);

        return _mapper.Map<ArticleDto>(article);
    }

    private int CountUserMaterials(Guid userId, ICollection<Material> materials)
    {
        int count = 0;
        foreach (var material in materials)
        {
            if (_unitOfWork.UserMaterialRepository
                .Exists(us => us.MaterialId == material.Id && us.UserId == userId))
                count++;
        }

        return count;
    }
}
