using AutoMapper;
using EducationPortal.Data.Entities;
using EducationPortal.Application.Dtos;

namespace EducationPortal.Application.Mappings;

public class SkillProfile : Profile
{
    public SkillProfile()
    {
        CreateMap<Skill, SkillDto>();
        CreateMap<SkillDto, Skill>()
            .ForMember(dest => dest.CourseSkills, opt => opt.Ignore());
    }
}
