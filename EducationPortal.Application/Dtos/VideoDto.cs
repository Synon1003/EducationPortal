namespace EducationPortal.Application.Dtos;

public record VideoDto(
    int Id,
    string Title,
    int Duration,
    string Quality
);

public record VideoCreateDto(
    string Title,
    int Duration,
    string Quality
);