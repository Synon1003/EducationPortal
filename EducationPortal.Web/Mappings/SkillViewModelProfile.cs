using AutoMapper;
using EducationPortal.Web.Models;
using EducationPortal.Application.Dtos;

namespace EducationPortal.Web.Mappings;

public class SkillViewModelProfile : Profile
{
    public SkillViewModelProfile()
    {
        CreateMap<SkillCreateViewModel, SkillCreateDto>();
        CreateMap<SkillDetailDto, SkillDetailViewModel>();
        CreateMap<SkillDto, SkillViewModel>().ReverseMap();
    }
}
