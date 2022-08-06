using Adesso.Application.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace Adesso.Application.Utilities.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            switch (error)
            {
                //case Excep e:
                //    // custom application error
                //    response.StatusCode = (int)HttpStatusCode.BadRequest;
                //    break;
                case KeyNotFoundException e:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var message = error?.Message;
            //var result = JsonSerializer.Serialize(new { message = error?.Message });
            //await response.WriteAsJsonAsync(new ErrorResult(message));
            var json = JsonConvert.SerializeObject(new ErrorResult(message));
            await response.WriteAsync(json);

        }
    }
}