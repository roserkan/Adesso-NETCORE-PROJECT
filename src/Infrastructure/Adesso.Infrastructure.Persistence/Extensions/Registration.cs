using Adesso.Application.Interfaces.Repositories;
using Adesso.Infrastructure.Persistence.Contexts;
using Adesso.Infrastructure.Persistence.Interceptors;
using Adesso.Infrastructure.Persistence.Repositories.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Adesso.Infrastructure.Persistence.Extensions;

public static class Registration
{
    public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<DbCommandInterceptor, QueryCommandInterceptor>();

        services.AddDbContext<AdessoDbContext>(conf =>
        {
            var connStr = configuration["AdessoDbConnectionString"].ToString();

            conf.UseSqlServer(connStr, opt =>
            {
                opt.EnableRetryOnFailure();
            }).AddInterceptors( new QueryCommandInterceptor());
        });


        //var seedData = new SeedData();
        //seedData.SeedAsync(configuration).GetAwaiter().GetResult();

        //services.AddScoped<ICategoryRepository, EFCategoryRepository>();
        //services.AddScoped<IMoneyPointRepository, EFMoneyPointRepository>();
        //services.AddScoped<IOrderRepository, EFOrderRepository>();
        //services.AddScoped<IOrderItemsRepository, EFOrderItemsRepository>();
        //services.AddScoped<IProductRepository, EFProductRepository>();
        //services.AddScoped<IUserRepository, EUserRepository>();
        //services.AddScoped<IUserDetailRepository, EUserDetailRepository>();

        services.AddScoped<IUnitOfWork, EFUnitOfWork>();






        return services;
    }
}
