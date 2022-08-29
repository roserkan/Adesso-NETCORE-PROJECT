using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Adesso.Infrastructure.Persistence.Extensions;

public static class AuthRegistration
{
    public static IServiceCollection ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["AuthConfig:Secret"]));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("AllowedAdmin", policy =>
                policy.RequireClaim(ClaimTypes.Role, "Admin"));
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("AllowedAdminAndMod", policy =>
                policy.RequireClaim(ClaimTypes.Role, "Admin", "Moderator")); // roller enum'dan alınabilir!
        });

        return services;
    }
}