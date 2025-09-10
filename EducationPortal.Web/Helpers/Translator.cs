using Microsoft.Extensions.Localization;
using System.Text.RegularExpressions;

namespace EducationPortal.Web.Helpers;

public static class Translator
{
    public static string Translate(IStringLocalizer localizer, string key)
    {
        string? parameter = null;
        LocalizedString? localized;

        var parameterMatch = Regex.Match(key, @"\((.*?)\)");
        if (parameterMatch.Success)
        {
            parameter = parameterMatch.Groups[1].Value;
            var normalizedKey = Regex.Replace(key, @"\(.*?\)", "_");
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
