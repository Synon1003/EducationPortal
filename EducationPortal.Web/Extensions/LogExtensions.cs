using Microsoft.AspNetCore.HttpLogging;
using Serilog;

namespace EducationPortal.Extensions;

public static class LogExtensions
{
    public static IHostBuilder AddSerilogLogging(this IHostBuilder builder)
    {
        builder.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration configuration) =>
        {
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext();
        });
        return builder;
    }

    public static IServiceCollection AddHttpLoggingWithFields(this IServiceCollection services)
    {
        services.AddHttpLogging(logging =>
        {
            logging.LoggingFields = HttpLoggingFields.RequestProperties
                | HttpLoggingFields.ResponsePropertiesAndHeaders;
        });
        return services;
    }
}