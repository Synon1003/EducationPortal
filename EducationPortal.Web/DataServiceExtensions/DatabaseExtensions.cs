using Microsoft.EntityFrameworkCore;
using EducationPortal.Data;
using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Data.Repositories;

namespace EducationPortal.DataServiceExtensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDbRepositories(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<EducationPortalDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<ICourseRepository, CourseRepository>()
                .AddScoped<ISkillRepository, SkillRepository>()
                .AddScoped<IMaterialRepository, MaterialRepository>();

        return services;
    }
}