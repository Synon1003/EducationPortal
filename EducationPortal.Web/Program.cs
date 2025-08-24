using EducationPortal.Extensions;
using EducationPortal.RepositoryTestEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbRepositories(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddViewModelMappers();
builder.Services.AddIdentityProviders();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser().Build();

    options.AddPolicy("NotAuthorized", policy =>
    {
        policy.RequireAssertion(context =>
        {
            return context.User.Identity is not null ?
                !context.User.Identity.IsAuthenticated : true;
        });
    });
});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});

builder.Services.AddControllersWithViews(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.UpdateDatabaseMigrationsAsync();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = "swagger";
    });

    app.MapRepositoryTestEndpoints();

}

app.UseHsts();
app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapGet("/", () => @"Education Portal test endpoints url: /
Swagger test endpoints url: /swagger
Application Guest url: /Home/Index
Application Login url: /Account/Login
Application User url: /Course/List");
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}");


app.Run();
