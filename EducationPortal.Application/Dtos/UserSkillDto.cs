namespace EducationPortal.Application.Dtos;

public record UserSkillDto(
    Guid UserId,
    int SkillId,
    string Name,
    int Level
);