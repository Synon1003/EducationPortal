using AutoMapper;
using EducationPortal.Web.Models;
using EducationPortal.Application.Dtos;

namespace EducationPortal.Web.Mappings;

public class UserCourseViewModelProfile : Profile
{
    public UserCourseViewModelProfile()
    {
        CreateMap<UserCourseDto, UserCourseViewModel>();
    }
}
