using EducationPortal.Web.LanguageResources;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System.Text.RegularExpressions;

namespace EducationPortal.Web.Extensions;

public static class HtmlHelperExtensionMethods
{
    public static string Translate(this IHtmlHelper helper, string key)
    {
        var services = helper.ViewContext.HttpContext.RequestServices;
        var factory = services.GetRequiredService<IStringLocalizerFactory>();
        var localizer = factory.Create("Resource", typeof(Resource).Assembly.FullName!);

        string? parameter = null;
        LocalizedString? localized;
        var parameterMatch = Regex.Match(key, @"\((.*?)\)");
        if (parameterMatch.Success)
        {
            parameter = parameterMatch.Groups[1].Value;
            string normalizedKey = Regex.Replace(key, @"\(.*?\)", "_");
            localized = localizer[normalizedKey];
        }
        else
        {
            localized = localizer[key];
        }

        if (localized.ResourceNotFound || string.IsNullOrEmpty(localized.Value))
            return key;

        return parameter == null ? localized.Value : string.Format(localized.Value, parameter);
    }
}