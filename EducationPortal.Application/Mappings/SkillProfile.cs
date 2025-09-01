using AutoMapper;
using EducationPortal.Data.Entities;
using EducationPortal.Application.Dtos;

namespace EducationPortal.Application.Mappings;

public class SkillProfile : Profile
{
    public SkillProfile()
    {
        CreateMap<Skill, SkillDto>();
        CreateMap<Skill, SkillDetailDto>()
            .ForCtorParam(ctorParamName: "AcquiredCount",
                opt => opt.MapFrom(src => src.UserSkills.Count))
            .ForCtorParam(ctorParamName: "AcquiredMaxLevel",
                opt => opt.MapFrom(src => src.UserSkills.Max(us => us.Level)));
        CreateMap<SkillCreateDto, Skill>();
    }
}
