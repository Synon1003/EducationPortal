using EducationPortal.Web.Mappings;
using EducationPortal.Web.Models;

namespace EducationPortal.Extensions;

public static class WebServiceExtensions
{
    public static IServiceCollection AddViewModelMappers(
        this IServiceCollection services)
    {
        services.AddAutoMapper(
            typeof(CourseViewModelProfile).Assembly,
            typeof(MaterialViewModelProfile).Assembly,
            typeof(VideoViewModelProfile).Assembly,
            typeof(PublicationViewModelProfile).Assembly,
            typeof(ArticleViewModelProfile).Assembly,
            typeof(SkillViewModelProfile).Assembly
        );

        return services;
    }
}