using AutoMapper;
using EducationPortal.Data.Entities;
using EducationPortal.Application.Dtos;

namespace EducationPortal.Application.Mappings;

public class UserSkillProfile : Profile
{
    public UserSkillProfile()
    {
        CreateMap<UserSkill, UserSkillDto>()
            .ForCtorParam(ctorParamName: "Name",
                opt => opt.MapFrom(src => src.Skill!.Name));
    }
}
