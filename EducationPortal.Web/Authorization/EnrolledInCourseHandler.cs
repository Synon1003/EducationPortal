using EducationPortal.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace EducationPortal.Web.Authorization;

public class EnrolledInCourseHandler : AuthorizationHandler<EnrolledInCourseRequirement, CourseAuthorizationResource>
{
    private readonly IUserCourseRepository _userCourseRepository;

    public EnrolledInCourseHandler(IUserCourseRepository userCourseRepository)
    {
        _userCourseRepository = userCourseRepository;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        EnrolledInCourseRequirement requirement,
        CourseAuthorizationResource resource)
    {
        var userCourse = await _userCourseRepository
            .GetByFilterAsync(uc => uc.UserId == resource.UserId && uc.CourseId == resource.CourseId);

        if (userCourse != null)
            context.Succeed(requirement);
    }
}

