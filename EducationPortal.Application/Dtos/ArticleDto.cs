namespace EducationPortal.Application.Dtos;

public record ArticleDto(
    int Id,
    DateOnly PublicationDate,
    string ResourceLink,
    int MaterialId,
    MaterialDto Material
);
