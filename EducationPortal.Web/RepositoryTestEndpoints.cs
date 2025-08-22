using EducationPortal.Data.Entities;
using EducationPortal.Data.Repositories.Interfaces;

namespace EducationPortal.RepositoryTestEndpoints;

public static class RepositoryTestEndpoints
{
    public static IEndpointRouteBuilder MapRepositoryTestEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/courses", async (ICourseRepository repository) =>
            TypedResults.Ok(await repository.GetAllCoursesWithSkillsAndMaterialsAsync()));

        app.MapGet("/courses/{id}", async (ICourseRepository repository, int id) =>
            TypedResults.Ok(await repository.GetCourseWithSkillsAsync(id)));

        app.MapGet("/courses/bymaterial/{materialId}", async (ICourseRepository repository, int materialId) =>
            TypedResults.Ok(await repository.GetAstronautsByMaterialIdAsync(materialId)));

        app.MapGet("/skills", async (ISkillRepository repository) =>
            TypedResults.Ok(await repository.GetAllAsync()));

        app.MapGet("/materials", async (IMaterialRepository repository) =>
            TypedResults.Ok(await repository.GetAllAsync()));

        app.MapGet("/materials/videos", async (IMaterialRepository repository) =>
            TypedResults.Ok(await repository.GetVideosAsync()));

        app.MapGet("/materials/publications", async (IMaterialRepository repository) =>
            TypedResults.Ok(await repository.GetPublicationsAsync()));

        app.MapGet("/materials/articles", async (IMaterialRepository repository) =>
            TypedResults.Ok(await repository.GetArticlesAsync()));

        return app;
    }
}
