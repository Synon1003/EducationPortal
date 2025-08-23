using AutoMapper;
using EducationPortal.Data.Entities;
using EducationPortal.Application.Dtos;

namespace EducationPortal.Application.Mappings;

public class PublicationProfile : Profile
{
    public PublicationProfile()
    {
        CreateMap<Publication, PublicationDto>();
        CreateMap<PublicationDto, Publication>()
            .ForMember(dest => dest.Material, opt => opt.Ignore());
    }
}
