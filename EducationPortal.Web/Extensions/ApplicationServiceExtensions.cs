using EducationPortal.Application.Services.Interfaces;
using EducationPortal.Application.Mappings;
using EducationPortal.Application.Services;

namespace EducationPortal.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddServices(
        this IServiceCollection services)
    {
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IMaterialService, MaterialService>();
        services.AddScoped<IUserService, UserService>();

        services.AddAutoMapper(
            typeof(CourseProfile).Assembly,
            typeof(UserCourseProfile).Assembly,
            typeof(UserSkillProfile).Assembly,
            typeof(SkillProfile).Assembly,
            typeof(MaterialProfile).Assembly,
            typeof(VideoProfile).Assembly,
            typeof(PublicationProfile).Assembly,
            typeof(ArticleProfile).Assembly
        );

        return services;
    }
}