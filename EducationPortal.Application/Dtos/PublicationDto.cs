namespace EducationPortal.Application.Dtos;

public record PublicationDto(
    int Id,
    string Title,
    string Authors,
    int Pages,
    string Format,
    int PublicationYear
);

public record PublicationCreateDto(
    string Title,
    string Authors,
    int Pages,
    string Format,
    int PublicationYear
);
