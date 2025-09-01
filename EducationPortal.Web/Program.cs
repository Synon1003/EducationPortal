using EducationPortal.Web.Middlewares;
using EducationPortal.Extensions;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbRepositories(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddViewModelMappers();
builder.Services.AddIdentityProviders();

if (builder.Environment.IsProduction())
{
    builder.Services.AddDataProtection()
        .PersistKeysToFileSystem(new DirectoryInfo("/keys"))
        .SetApplicationName("EducationPortal");
}

builder.Services.AddAuthorization();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});

builder.Services.AddControllersWithViews(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    await app.UpdateDatabaseMigrationsAsync();

    app.UseHsts();
    app.UseHttpsRedirection();
}
else if (app.Environment.IsProduction())
{
    await app.InitializeDatabaseAsync();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
