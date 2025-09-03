using Serilog;
using System.Diagnostics;

namespace EducationPortal.Web.Middlewares;

public class HttpLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public HttpLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var sw = Stopwatch.StartNew();

        var request = context.Request;
        var requestInfo = $"{request.Method} {request.Path}{request.QueryString}";

        await _next(context);

        sw.Stop();

        var response = context.Response;
        var responseInfo = $"{response.StatusCode} {response.ContentType}";

        Log.Information(
            "HTTP {Request} -> {Response} | Duration: {Duration}ms",
            requestInfo,
            responseInfo,
            sw.ElapsedMilliseconds
        );
    }
}

