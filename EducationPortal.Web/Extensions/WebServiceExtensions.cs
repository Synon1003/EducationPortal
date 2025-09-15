using EducationPortal.Data.Entities;
using EducationPortal.Web.Helpers;
using EducationPortal.Web.Authorization;
using EducationPortal.Web.Mappings;
using EducationPortal.Web.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace EducationPortal.Extensions;

public static class WebServiceExtensions
{
    public static IServiceCollection AddWebServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAutoMapper(
            typeof(CourseViewModelProfile).Assembly,
            typeof(UserCourseViewModelProfile).Assembly,
            typeof(UserSkillViewModelProfile).Assembly,
            typeof(MaterialViewModelProfile).Assembly,
            typeof(VideoViewModelProfile).Assembly,
            typeof(PublicationViewModelProfile).Assembly,
            typeof(ArticleViewModelProfile).Assembly,
            typeof(SkillViewModelProfile).Assembly
        );

        services.Configure<AppearanceOptions>(configuration.GetSection("Appearance"));

        services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();
        services.AddScoped<IAuthorizationHandler, EnrolledInCourseHandler>();

        return services;
    }
}