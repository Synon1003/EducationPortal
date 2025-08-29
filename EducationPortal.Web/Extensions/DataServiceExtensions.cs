using Microsoft.EntityFrameworkCore;
using EducationPortal.Data;
using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Data.Repositories;
using EducationPortal.Data.Entities;
using Microsoft.AspNetCore.Identity;
using EducationPortal.Web.Helpers;

namespace EducationPortal.Extensions;

public static class DataServiceExtensions
{
    public static IServiceCollection AddDbRepositories(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<EducationPortalDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICourseRepository, CourseRepository>()
                .AddScoped<IUserCourseRepository, UserCourseRepository>()
                .AddScoped<IUserMaterialRepository, UserMaterialRepository>()
                .AddScoped<ISkillRepository, SkillRepository>()
                .AddScoped<IMaterialRepository, MaterialRepository>();

        return services;
    }

    public static IServiceCollection AddIdentityProviders(
        this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireDigit = false;
            options.Password.RequiredUniqueChars = 5;
        })
        .AddEntityFrameworkStores<EducationPortalDbContext>();

        services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();

        return services;
    }
}