namespace EducationPortal.Application.Dtos;

public record CourseListDto(
    int Id,
    string Name,
    string Description,
    List<SkillDto> Skills
);

public record CourseDetailDto(
    int Id,
    string Name,
    string Description,
    List<SkillDto> Skills,
    List<MaterialDto> Materials,
    string CreatedBy
);

public record CourseCreateDto(
    string Name,
    string Description,
    List<SkillCreateDto> Skills,
    List<VideoCreateDto> Videos,
    List<PublicationCreateDto> Publications,
    List<ArticleCreateDto> Articles,
    Guid? CreatedBy
);