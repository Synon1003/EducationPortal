namespace EducationPortal.Application.Dtos;

public record SkillDto(int Id, string Name);
public record SkillDetailDto(int Id, string Name, int AcquiredCount, int AcquiredMaxLevel);
public record SkillCreateDto(string Name);