using Adesso.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adesso.Application.CrossCuttingConcerns.Logging;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        context.Response.StatusCode = 200;
        var logDetailObject = CreateLogDetailHelper.CreateLogDetail(LogLevel.Informational, context, "Başarılı işlem");
        string logDetail = JsonConvert.SerializeObject(logDetailObject);
        Logger.Log(LogTypes.File, logDetail);
        await _next(context);
    }
}