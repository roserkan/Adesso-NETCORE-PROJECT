using Microsoft.AspNetCore.Builder;

namespace Adesso.Application.CrossCuttingConcerns.Exceptions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}