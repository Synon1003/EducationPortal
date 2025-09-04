using EducationPortal.Web.Helpers;

namespace EducationPortal.Extensions;

public static class LanguageExtensions
{
    public static WebApplication UseRequestLanguages(this WebApplication app)
    {
        var supportedCultures = new[] { "en", "hu" };
        var localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture("en")
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);

        localizationOptions.RequestCultureProviders.Insert(0, new UserProfileRequestCultureProvider());

        app.UseRequestLocalization(localizationOptions);
        return app;
    }
}