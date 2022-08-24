using Adesso.Application.CrossCuttingConcerns.Exceptions;
using Adesso.Application.CrossCuttingConcerns.Logging;
using Adesso.Application.Extensions;
using Adesso.Infrastructure.Persistence.Extensions;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = null;
    });
    //.AddFluentValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddApplicationRegistration();
builder.Services.AddInfrastructureRegistration(builder.Configuration);
builder.Services.ConfigureAuth(builder.Configuration);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AllowedAdmin", policy =>
        policy.RequireClaim(ClaimTypes.Role, "Admin", "User"));
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHttpsRedirection();
app.UseStaticFiles();
//app.ErrorHandlerMiddleware();
app.ConfigureCustomExceptionMiddleware();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.ConfigureCustomLoggingMiddleware();
app.Run();
