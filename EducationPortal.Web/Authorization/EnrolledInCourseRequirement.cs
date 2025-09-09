using Microsoft.AspNetCore.Authorization;

namespace EducationPortal.Web.Authorization;

public class EnrolledInCourseRequirement : IAuthorizationRequirement
{ }

public class CourseAuthorizationResource
{
    public Guid UserId { get; }
    public int CourseId { get; }

    public CourseAuthorizationResource(Guid userId, int courseId)
    {
        UserId = userId;
        CourseId = courseId;
    }
}