using Eximia.CsharpCourse.API.Infrastructure.Filters;
using Eximia.CsharpCourse.SeedWork.Settings;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace Eximia.CsharpCourse.API.Infrastructure;

internal static class ServicesExtensions
{
    internal static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<StockApiSettings>(configuration.GetSection("StockApi"));
        return services;
    }

    internal static IServiceCollection ConfigureFlurl(this IServiceCollection services)
    {
        FlurlHttp.Clients.WithDefaults(settings =>
        {
            settings.AllowHttpStatus("400-404,422");
        });

        return services;
    }

    internal static IServiceCollection AddHealth(this IServiceCollection services, IConfiguration configuration)
    {
        var hcBuilder = services.AddHealthChecks();
        hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy(), new string[] { "ready" });
        return services;
    }

    internal static IServiceCollection AddControllersWithFilter(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<HttpGlobalExceptionFilter>();
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

        return services;
    }

    internal static IServiceCollection RemoveModelValidation(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(opt =>
        {
            opt.SuppressModelStateInvalidFilter = true;
        });

        return services;
    }

    internal static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(x =>
        {
            x.SwaggerDoc("v1", new OpenApiInfo { Title = "Eximia C# Course API" });
        });

        return services;
    }
}
