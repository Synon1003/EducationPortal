using EducationPortal.Extensions;
using EducationPortal.Web.Middlewares;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddSerilogLogging();
builder.Services.AddLocalization(options => options.ResourcesPath = "LanguageResources");

builder.Services.AddDataServices(builder.Configuration)
                .AddIdentityProviders();
builder.Services.AddApplicationServices();
builder.Services.AddWebServices(builder.Configuration);

builder.Services.AddAuthorization();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Home/Forbidden";
});

builder.Services.AddControllersWithViews(
    options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute())
).AddViewLocalization()
.AddDataAnnotationsLocalization();

if (builder.Environment.IsProduction())
{
    builder.Services.AddDataProtection()
        .PersistKeysToFileSystem(new DirectoryInfo("/keys"))
        .SetApplicationName("EducationPortal");
}

var app = builder.Build();
app.UseExceptionHandler("/Home/Error");
app.UseMiddleware<HttpLoggingMiddleware>();

if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();

    app.UseHsts();
    app.UseHttpsRedirection();
}
else if (app.Environment.IsProduction())
{
    await app.InitializeDatabaseFromContainerAsync();
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseRequestLanguages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
