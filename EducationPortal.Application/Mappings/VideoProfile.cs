using AutoMapper;
using EducationPortal.Data.Entities;
using EducationPortal.Application.Dtos;

namespace EducationPortal.Application.Mappings;

public class VideoProfile : Profile
{
    public VideoProfile()
    {
        CreateMap<Video, VideoDto>();
        CreateMap<VideoDto, Video>()
            .ForMember(dest => dest.Material, opt => opt.Ignore());
    }
}
