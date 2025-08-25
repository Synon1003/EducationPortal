namespace EducationPortal.Application.Dtos;

public record ArticleDto(
    int Id,
    string Title,
    DateOnly PublicationDate,
    string ResourceLink
);

public record ArticleCreateDto(
    string Title,
    DateOnly PublicationDate,
    string ResourceLink
);
