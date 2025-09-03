using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using EducationPortal.Data;

namespace EducationPortal.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitializeDatabaseFromContainerAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<EducationPortalDbContext>();
        var env = scope.ServiceProvider.GetRequiredService<IHostEnvironment>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<EducationPortalDbContext>>();

        const int maxRetries = 5;
        const int delaySeconds = 10;

        for (int attempt = 1; attempt <= maxRetries; attempt++)
        {
            try
            {
                logger.LogInformation("Attempt {Attempt}: Ensuring database created...", attempt);
                await EnsureDatabaseAsync(dbContext);

                logger.LogInformation("Attempt {Attempt}: Applying migrations...", attempt);
                await RunMigrationsAsync(dbContext);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Database connection attempt {Attempt} failed.", attempt);

                if (attempt == maxRetries)
                {
                    logger.LogError("All {MaxRetries} database connection attempts failed.", maxRetries);
                    throw;
                }

                await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
            }
        }
    }

    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<EducationPortalDbContext>();

        await EnsureDatabaseAsync(dbContext);
        await RunMigrationsAsync(dbContext);
    }

    private static async Task EnsureDatabaseAsync(EducationPortalDbContext dbContext)
    {
        var dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();

        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            if (!await dbCreator.ExistsAsync())
                await dbCreator.CreateAsync();
        });
    }

    private static async Task RunMigrationsAsync(EducationPortalDbContext dbContext)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync();
            await dbContext.Database.MigrateAsync();
            await transaction.CommitAsync();
        });
    }
}