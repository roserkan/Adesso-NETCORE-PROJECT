using Adesso.Application.CrossCuttingConcerns.Caching;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace Adesso.Application.Helpers.MediatrPiplines;

public class CacheBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ICacheManager _cacheManager;
    private readonly IHttpContextAccessor _httpContext;

    public CacheBehaviour(ICacheManager cacheManager, IHttpContextAccessor httpContext)
    {
        _cacheManager = cacheManager;
        _httpContext = httpContext;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        //{ Adesso.Application.Features.Category.Commands.Create}
        var context = _httpContext.HttpContext;
        string methodType = context.Request.Method;
        string endpoint = context.Request.Host.Value + context.Request.Path;
        Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
        foreach (var header in context.Request.Headers)
        {
            requestHeaders.Add(header.Key, header.Value);
        }
        var token = requestHeaders["Authentication"];

        string cacheKey = $"{methodType}-{endpoint}-{token}";
        //string type = cacheKey.Split(".")[3]; 
        bool isQuerie = cacheKey.Contains("GET");


        if (isQuerie)
        {
            bool isAdd = _cacheManager.IsAdd(cacheKey);
            if (isAdd)
            {
                var cacheData = _cacheManager.Get<TResponse>(cacheKey);
                return cacheData;
            }

            var data = await next();
            _cacheManager.Add(cacheKey, data, 10);
            return data;
        }

        _cacheManager.RemoveByPattern(methodType);
        var response = await next();
        return response;
    }
}
