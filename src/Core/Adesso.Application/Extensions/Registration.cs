using Adesso.Application.CrossCuttingConcerns.Caching;
using Adesso.Application.CrossCuttingConcerns.Caching.Microsoft;
using Adesso.Application.Pipelines.Caching;
using Adesso.Application.Pipelines.SaveChanges;
using Adesso.Application.Pipelines.Validation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Adesso.Application.Extensions;

public static class Registration
{
    public static IServiceCollection AddApplicationRegistration(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(assembly);
        services.AddAutoMapper(assembly);
        //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(assembly);
        services.AddMemoryCache();
        services.AddSingleton<ICacheManager, MemoryCacheManager>();


        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(SaveChangesBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheBehaviour<,>));


        return services;
    }
}