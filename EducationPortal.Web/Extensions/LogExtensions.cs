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
}