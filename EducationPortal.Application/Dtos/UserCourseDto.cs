namespace EducationPortal.Application.Dtos;

public record UserCourseDto(
    Guid UserId,
    int courseId,
    bool IsCompleted,
    int ProgressPercentage
);