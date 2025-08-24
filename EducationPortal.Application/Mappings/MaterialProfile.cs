using AutoMapper;
using EducationPortal.Data.Entities;
using EducationPortal.Application.Dtos;

namespace EducationPortal.Application.Mappings;

public class MaterialProfile : Profile
{
    public MaterialProfile()
    {
        CreateMap<Material, MaterialDto>();
        CreateMap<MaterialCreateDto, Material>()
           .ForMember(dest => dest.CourseMaterials, opt => opt.Ignore())
           .ForMember(dest => dest.Video, opt => opt.Ignore())
           .ForMember(dest => dest.Publication, opt => opt.Ignore())
           .ForMember(dest => dest.Article, opt => opt.Ignore());
    }
}
