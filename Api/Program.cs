using Core.Extensions;
using Infrastructure.Config;
using Infrastructure.Middlewares;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

AppConfig.Init(builder.Configuration);

builder.Services.AddControllers()
                .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddDbContext();
builder.Services.AddRepositories();
builder.Services.AddServices();

builder.Services.ConfigureAuthorization();
builder.Services.ConfigureSwagger();

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSwagger(c =>
{
    c.RouteTemplate = "api/swagger/{documentName}/swagger.json";
});

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "Travel");
    c.RoutePrefix = "api/swagger";
    c.DocumentTitle = $"Hackaton {app.Environment.EnvironmentName} - Swagger UI";
});

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();