using AutoMapper;
using EducationPortal.Web.Models;
using EducationPortal.Application.Dtos;

namespace EducationPortal.Web.Mappings;

public class CourseViewModelProfile : Profile
{
    public CourseViewModelProfile()
    {
        CreateMap<CourseListDto, CourseListViewModel>()
            .ForMember(dest => dest.Skills, opt =>
                opt.MapFrom(src => src.Skills.Select(s => s.Name).ToList()));

        CreateMap<CourseDetailDto, CourseDetailViewModel>()
            .ForMember(dest => dest.Skills, opt =>
                opt.MapFrom(src => src.Skills.Select(s => s.Name).ToList()))
            .ForMember(dest => dest.Materials, opt =>
                opt.MapFrom(src => src.Materials.Select(s => s.Title).ToList()));
    }
}
