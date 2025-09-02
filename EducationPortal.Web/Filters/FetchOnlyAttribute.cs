using EducationPortal.Application.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EducationPortal.Web.Filters;

public class FetchOnlyAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var request = context.HttpContext.Request;
        if (!request.Headers.ContainsKey("X-Fetch-Request"))
        {
            throw new BadRequestException("The request could not be processed. Please make sure the data is correct.");
        }

        base.OnActionExecuting(context);
    }
}