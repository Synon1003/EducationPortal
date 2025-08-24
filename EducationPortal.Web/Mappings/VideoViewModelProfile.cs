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
    }
}
