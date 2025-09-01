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
                opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt =>
                opt.MapFrom(src => src.CreatedBy));

        CreateMap<CourseCreateViewModel, CourseCreateDto>()
            .ConstructUsing(src => new CourseCreateDto(
                src.Name,
                src.Description,
                src.Skills.Select(s => new SkillCreateDto(s.Name)).ToList(),
                src.Videos.Select(v => new VideoCreateDto(
                    v.Title, v.Duration, v.Quality)).ToList(),
                src.Publications.Select(v => new PublicationCreateDto(
                    v.Title, v.Authors, v.Pages, v.Format, v.PublicationYear)).ToList(),
                src.Articles.Select(v => new ArticleCreateDto(
                    v.Title, v.PublicationDate, v.ResourceLink)).ToList(),
                src.LoadedVideos.Select(v => new VideoDto(
                    v.Id, v.Title, v.Duration, v.Quality)).ToList(),
                src.LoadedPublications.Select(p => new PublicationDto(
                    p.Id, p.Title, p.Authors, p.Pages, p.Format, p.PublicationYear)).ToList(),
                src.LoadedArticles.Select(a => new ArticleDto(
                    a.Id, a.Title, a.PublicationDate, a.ResourceLink)).ToList(),
                null
            ));
    }
}
