namespace EducationPortal.Application.Dtos;

public record MaterialDto(int Id, string Title, string Type);
public record MaterialCreateDto(string Title, string Type);
