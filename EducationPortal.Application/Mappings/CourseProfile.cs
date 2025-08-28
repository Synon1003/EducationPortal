using AutoMapper;
using EducationPortal.Data.Entities;
using EducationPortal.Application.Dtos;

namespace EducationPortal.Application.Mappings;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<Course, CourseListDto>();
        CreateMap<Course, CourseDetailDto>()
            .ForCtorParam(ctorParamName: "CreatedByUserName",
                opt => opt.MapFrom(src => src.CreatedByUser.UserName));
    }
}
