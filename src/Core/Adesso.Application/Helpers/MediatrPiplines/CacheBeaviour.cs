using Adesso.Application.CrossCuttingConcerns.Caching;
using Adesso.Application.Interfaces.Repositories;
using MediatR;

namespace Adesso.Application.Helpers.MediatrPiplines;

public class CacheBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ICacheManager _cacheManager;

    public CacheBehaviour(ICacheManager cacheManager)
    {
        _cacheManager = cacheManager;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        //{ Adesso.Application.Features.Category.Commands.Create}
        string cacheKey = request.ToString();
        string type = cacheKey.Split(".")[3]; 
        bool isQuerie = cacheKey.Contains("Querie");

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
        _cacheManager.RemoveByPattern(type);
        var response = await next();
        return response;
    }
}
