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

        CreateMap<CourseCreateViewModel, CourseCreateDto>()
            .ConstructUsing(src => new CourseCreateDto(
                src.Name,
                src.Description,
                src.Skills.Select(s => new SkillCreateDto(s.Name)).ToList(),
                src.Videos.Select(v => new VideoCreateDto(v.Duration, v.Quality,
                    new MaterialCreateDto(v.Title, "Video"))).ToList(),
                src.Publications.Select(v => new PublicationCreateDto(v.Authors, v.Pages, v.Format, v.PublicationYear,
                    new MaterialCreateDto(v.Title, "Publication"))).ToList(),
                src.Articles.Select(v => new ArticleCreateDto(v.PublicationDate, v.ResourceLink,
                    new MaterialCreateDto(v.Title, "Article"))).ToList()

            ));
    }
}
