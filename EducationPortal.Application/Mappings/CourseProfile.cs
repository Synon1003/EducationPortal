using AutoMapper;
using EducationPortal.Data.Entities;
using EducationPortal.Application.Dtos;

namespace EducationPortal.Application.Mappings;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<Course, CourseListDto>()
            .ForCtorParam(ctorParamName: "CreatedBy",
                opt => opt.MapFrom(src => src.CreatedByUser.UserName));
        CreateMap<Course, CourseDetailDto>()
            .ForCtorParam(ctorParamName: "CreatedBy",
                opt => opt.MapFrom(src => src.CreatedByUser.UserName));
    }
}
