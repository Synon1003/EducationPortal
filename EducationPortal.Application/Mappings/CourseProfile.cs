using AutoMapper;
using EducationPortal.Data.Entities;
using EducationPortal.Application.Dtos;

namespace EducationPortal.Application.Mappings;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<Course, CourseListDto>()
            .ForCtorParam(ctorParamName: "Skills", opt => opt.MapFrom(src => src.CourseSkills.Select(cs => cs.Skill).ToList()));

        CreateMap<CourseListDto, Course>()
            .ForMember(dest => dest.CourseSkills, opt => opt.Ignore());


        CreateMap<Course, CourseDetailDto>()
            .ForCtorParam(ctorParamName: "Skills", opt => opt.MapFrom(src => src.CourseSkills.Select(cs => cs.Skill).ToList()))
            .ForCtorParam(ctorParamName: "Materials", opt => opt.MapFrom(src => src.CourseMaterials.Select(cs => cs.Material).ToList()));

        CreateMap<CourseDetailDto, Course>()
            .ForMember(dest => dest.CourseSkills, opt => opt.Ignore())
            .ForMember(dest => dest.CourseMaterials, opt => opt.Ignore());
    }
}
