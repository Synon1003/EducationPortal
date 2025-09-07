namespace EducationPortal.Application.Dtos;

public record UserCourseDto(
    Guid UserId,
    int CourseId,
    bool IsCompleted,
    int ProgressPercentage
);