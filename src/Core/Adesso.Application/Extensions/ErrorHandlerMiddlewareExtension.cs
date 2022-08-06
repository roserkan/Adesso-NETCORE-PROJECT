using Adesso.Application.Utilities.Middlewares;
using Microsoft.AspNetCore.Builder;


namespace Adesso.Application.Extensions;

public static class ErrorHandlerMiddlewareExtension
{
    public static IApplicationBuilder ErrorHandlerMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlerMiddleware>();

    }
}
