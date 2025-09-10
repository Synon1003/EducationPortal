using EducationPortal.Web.Helpers;
using EducationPortal.Web.LanguageResources;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;

namespace EducationPortal.Web.Extensions;

public static class HtmlHelperExtensionMethods
{
    public static string Translate(this IHtmlHelper helper, string key)
    {
        var services = helper.ViewContext.HttpContext.RequestServices;
        var factory = services.GetRequiredService<IStringLocalizerFactory>();
        var localizer = factory.Create("Resource", typeof(Resource).Assembly.FullName!);

        return Translator.Translate(localizer, key);
    }
}