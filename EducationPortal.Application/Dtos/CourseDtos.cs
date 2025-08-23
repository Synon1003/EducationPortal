namespace EducationPortal.Application.Dtos;

public interface ICourseDto
{
    public string Name { get; init; }
    public string Description { get; init; }
}

public record CourseListDto(
    int Id,
    string Name,
    string Description,
    List<SkillDto> Skills
) : ICourseDto;

public record CourseDetailDto(
    int Id,
    string Name,
    string Description,
    List<SkillDto> Skills,
    List<MaterialDto> Materials
) : ICourseDto;
