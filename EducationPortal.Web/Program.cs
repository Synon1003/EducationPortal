using EducationPortal.DataServiceExtensions;
using EducationPortal.RepositoryTestEndpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbRepositories(builder.Configuration);
builder.Services.AddIdentityProviders();

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
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

app.MapGet("/", () => "Education Portal");
app.UseRouting();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}");


app.Run();
