using Microsoft.AspNetCore.Localization;

namespace EducationPortal.Web.Helpers;

public class UserProfileRequestCultureProvider : IRequestCultureProvider
{
    public async Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
    {

        var user = httpContext.User;
        if (user?.Identity?.IsAuthenticated == true)
        {
            var lang = user.FindFirst("Language")?.Value;
            if (!string.IsNullOrEmpty(lang))
                return new ProviderCultureResult(lang, lang);
        }

        return await Task.FromResult<ProviderCultureResult?>(null);
    }
}