using Adesso.Application.Extensions;
using Adesso.Infrastructure.Persistence.Extensions;
using FluentValidation.AspNetCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = null;
    })
    .AddFluentValidation();
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

app.UseHttpsRedirection();
app.ErrorHandlerMiddleware();
app.UseAuthentication();
app.UseAuthorization(); // 401
app.MapControllers();


app.Run();
