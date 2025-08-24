using AutoMapper;
using EducationPortal.Web.Models;
using EducationPortal.Application.Dtos;

namespace EducationPortal.Web.Mappings;

public class PublicationViewModelProfile : Profile
{
    public PublicationViewModelProfile()
    {
        CreateMap<PublicationDto, PublicationViewModel>()
            .ForMember(dest => dest.Title, opt =>
                opt.MapFrom(src => src.Material.Title))
            .ForMember(dest => dest.Authors, opt =>
                opt.MapFrom(src => String.Join(", ", src.Authors)));

        CreateMap<PublicationCreateViewModel, PublicationCreateDto>()
            .ConstructUsing(src => new PublicationCreateDto(
                src.Authors,
                src.Pages,
                src.Format,
                src.PublicationYear,
                new MaterialCreateDto(src.Title, "Publication")));
    }
}
