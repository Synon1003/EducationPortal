using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EducationPortal.Web.Filters;

public class FetchOnlyAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var request = context.HttpContext.Request;
        if (!request.Headers.ContainsKey("X-Fetch-Request"))
        {
            context.Result = new BadRequestResult();
        }

        base.OnActionExecuting(context);
    }
}