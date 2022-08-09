using Adesso.Application.Helpers.MediatrPiplines;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Infrastructure.Persistence.Contexts;
using Adesso.Infrastructure.Persistence.Repositories.EFCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Adesso.Infrastructure.Persistence.Extensions;

public static class Registration
{
    public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AdessoDbContext>(conf =>
        {
            var connStr = configuration["AdessoDbConnectionString"].ToString();
            Console.WriteLine(connStr);
            conf.UseSqlServer(connStr, opt =>
            {
                opt.EnableRetryOnFailure();
            });
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
