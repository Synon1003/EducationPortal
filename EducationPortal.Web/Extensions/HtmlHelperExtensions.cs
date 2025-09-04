using EducationPortal.Web.LanguageResources;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;

namespace EducationPortal.Web.Extensions;

public static class HtmlHelperExtensionMethods
{
    public static string Translate(this IHtmlHelper helper, string key, string? param = null)
    {
        var services = helper.ViewContext.HttpContext.RequestServices;
        var factory = services.GetRequiredService<IStringLocalizerFactory>();
        var localizer = factory.Create("Resource", typeof(Resource).Assembly.FullName!);

        var localized = localizer[key];

        if (localized.ResourceNotFound || string.IsNullOrEmpty(localized.Value))
            return key;

        return param == null ? localized.Value : string.Format(localized.Value, param);
    }
}