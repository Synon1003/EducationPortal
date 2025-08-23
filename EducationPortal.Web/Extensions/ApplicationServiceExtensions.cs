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

        services.AddAutoMapper(
            typeof(CourseProfile).Assembly,
            typeof(SkillProfile).Assembly,
            typeof(MaterialProfile).Assembly
        );

        return services;
    }
}