using AutoMapper;
using EducationPortal.Web.Models;
using EducationPortal.Application.Dtos;

namespace EducationPortal.Web.Mappings;

public class PublicationViewModelProfile : Profile
{
    public PublicationViewModelProfile()
    {
        CreateMap<PublicationDto, PublicationViewModel>().ReverseMap();

        CreateMap<PublicationCreateViewModel, PublicationCreateDto>()
            .ConstructUsing(src => new PublicationCreateDto(
                src.Title,
                src.Authors,
                src.Pages,
                src.Format,
                src.PublicationYear
            ));
    }
}
