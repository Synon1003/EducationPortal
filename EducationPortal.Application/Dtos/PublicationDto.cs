namespace EducationPortal.Application.Dtos;

public record PublicationDto(
    int Id,
    string Authors,
    int Pages,
    string Format,
    int PublicationYear,
    int MaterialId,
    MaterialDto Material
);

public record PublicationCreateDto(
    string Authors,
    int Pages,
    string Format,
    int PublicationYear,
    MaterialCreateDto Material
);
