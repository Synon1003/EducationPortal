using AutoMapper;
using EducationPortal.Web.Models;
using EducationPortal.Application.Dtos;

namespace EducationPortal.Web.Mappings;

public class VideoViewModelProfile : Profile
{
    public VideoViewModelProfile()
    {
        CreateMap<VideoDto, VideoViewModel>();

        CreateMap<VideoCreateViewModel, VideoCreateDto>()
            .ConstructUsing(src => new VideoCreateDto(
                src.Title,
                src.Duration,
                src.Quality
            ));
    }
}
