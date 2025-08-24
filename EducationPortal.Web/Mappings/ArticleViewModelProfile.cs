using AutoMapper;
using EducationPortal.Web.Models;
using EducationPortal.Application.Dtos;

namespace EducationPortal.Web.Mappings;

public class ArticleViewModelProfile : Profile
{
    public ArticleViewModelProfile()
    {
        CreateMap<ArticleDto, ArticleViewModel>()
            .ForMember(dest => dest.Title, opt =>
                opt.MapFrom(src => src.Material.Title))
            .ForMember(dest => dest.PublicationDate, opt =>
                opt.MapFrom(src => src.PublicationDate.ToString("d")));
    }
}
