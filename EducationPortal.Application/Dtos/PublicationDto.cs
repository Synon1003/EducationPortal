namespace EducationPortal.Application.Dtos;

public record PublicationDto(
    int Id,
    string Authors,
    int Pages,
    string Format,
    int PublicationYear,
    int materialId
);
