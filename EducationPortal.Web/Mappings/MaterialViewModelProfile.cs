using AutoMapper;
using EducationPortal.Web.Models;
using EducationPortal.Application.Dtos;

namespace EducationPortal.Web.Mappings;

public class MaterialViewModelProfile : Profile
{
    public MaterialViewModelProfile()
    {
        CreateMap<MaterialDto, MaterialViewModel>();
        CreateMap<MaterialCreateViewModel, MaterialCreateDto>();
    }
}
