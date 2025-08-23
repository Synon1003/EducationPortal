namespace EducationPortal.Application.Dtos;

public record VideoDto(
    int Id,
    int Duration,
    string Quality,
    int materialId
);