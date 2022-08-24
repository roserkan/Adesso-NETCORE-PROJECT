using Microsoft.AspNetCore.Builder;

namespace Adesso.Application.CrossCuttingConcerns.Logging;

public static class LoggingMiddlewareExtensions
{
    public static void ConfigureCustomLoggingMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<LoggingMiddleware>();
    }
}