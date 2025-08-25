using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Application.Services.Interfaces;

namespace EducationPortal.RepositoryTestEndpoints;

public static class RepositoryTestEndpoints
{
    public static IEndpointRouteBuilder MapRepositoryTestEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/courses", async (ICourseService service) =>
            TypedResults.Ok(await service.GetAllCoursesWithSkillsAsync()));

        app.MapGet("/courses/{id}", async (ICourseService service, int id) =>
            TypedResults.Ok(await service.GetCourseWithSkillsAndMaterialsByIdAsync(id)));

        app.MapGet("/courses/bymaterial/{materialId}", async (ICourseService service, int materialId) =>
            TypedResults.Ok(await service.GetCoursesByMaterialIdAsync(materialId)));

        app.MapGet("/skills", async (ISkillRepository repository) =>
            TypedResults.Ok(await repository.GetAllAsync()));

        app.MapGet("/materials", async (IMaterialService service) =>
            TypedResults.Ok(await service.GetAllMaterialsAsync()));

        app.MapGet("/materials/videos", async (IMaterialRepository repository) =>
            TypedResults.Ok(await repository.GetAllVideosAsync()));

        app.MapGet("/materials/publications", async (IMaterialRepository repository) =>
            TypedResults.Ok(await repository.GetAllPublicationsAsync()));

        app.MapGet("/materials/articles", async (IMaterialRepository repository) =>
            TypedResults.Ok(await repository.GetAllArticlesAsync()));

        app.MapGet("/materials/bycourse/{courseId}", async (IMaterialService service, int courseId) =>
            TypedResults.Ok(await service.GetMaterialsByCourseIdAsync(courseId)));

        return app;
    }
}
