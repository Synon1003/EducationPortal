using AutoMapper;
using EducationPortal.Web.Models;
using EducationPortal.Application.Dtos;

namespace EducationPortal.Web.Mappings;

public class VideoViewModelProfile : Profile
{
    public VideoViewModelProfile()
    {
        CreateMap<VideoDto, VideoViewModel>()
            .ForMember(dest => dest.Title, opt =>
                opt.MapFrom(src => src.Material.Title));

        CreateMap<VideoCreateViewModel, VideoCreateDto>()
            .ConstructUsing(src => new VideoCreateDto(
                src.Duration,
                src.Quality,
                new MaterialCreateDto(src.Title, "Video")));
    }
}
