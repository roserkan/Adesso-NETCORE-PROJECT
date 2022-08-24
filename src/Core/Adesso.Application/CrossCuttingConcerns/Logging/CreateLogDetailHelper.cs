using Adesso.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Adesso.Application.CrossCuttingConcerns.Logging;

public static class CreateLogDetailHelper
{
    public static LogDetail CreateLogDetail(LogLevel logLevel, HttpContext context, string message)
    {
        string id = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        string email = context.User.FindFirst(ClaimTypes.Email)?.Value;
        string path = context.Request.Path;
        string methodType = context.Request.Method;
        string statusCode = context.Response.StatusCode.ToString();

        var logDetail = new LogDetail
        {
            LogLevel = logLevel,
            UserId = id,
            UserEmailAddress = email,
            Path = path,
            MethodType = methodType,
            StatusCode = statusCode,
            Message = message,
            Date = DateTime.Now.ToString()
        };

        return logDetail;
    }
}
