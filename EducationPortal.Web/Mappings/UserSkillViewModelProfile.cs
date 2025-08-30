using AutoMapper;
using EducationPortal.Web.Models;
using EducationPortal.Application.Dtos;

namespace EducationPortal.Web.Mappings;

public class UserSkillViewModelProfile : Profile
{
    public UserSkillViewModelProfile()
    {
        CreateMap<UserSkillDto, UserSkillViewModel>();
    }
}
