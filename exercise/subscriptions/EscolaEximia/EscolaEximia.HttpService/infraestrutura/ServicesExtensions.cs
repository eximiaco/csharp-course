using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Filters;
using Serilog.Sinks.OpenTelemetry;

namespace EscolaEximia.HttpService.infraestrutura;

public static class ServicesExtensions
{
    public static IServiceCollection AddSwaggerDoc(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        return services;
    }
        
    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(1, 0);
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ReportApiVersions = true;
        });
        return services;
    }

    public static IServiceCollection AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(
            o =>
                o.AddPolicy(
                    "default",
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    }
                )
        );
        return services;
    }
        
    public static IServiceCollection AddHealth(this IServiceCollection services, IConfiguration configuration)
    {
        var hcBuilder = services.AddHealthChecks();
        hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy(), new string[] { "ready" });
        // hcBuilder
        //     .AddNpgSql(
        //         configuration.GetConnectionString(Ambient.DatabaseConnectionName)!,
        //         name: "integration-store-check",
        //         tags: new string[] {"IntegrationStoreCheck", "health"});
        return services;
    }

    public static IServiceCollection AddWorkersServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
        
    public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
    {
        // services
        //     .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //     .AddMicrosoftIdentityWebApi(configuration);
        return services;
    }
        
    public static IServiceCollection AddLogs(this IServiceCollection services, IConfiguration configuration, string serviceName)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .Enrich.WithThreadId()
            .Enrich.WithProperty("service_name",serviceName )
            //.Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.Hosting.Diagnostics"))
            //.Filter.ByExcluding(Matching.FromSource("Microsoft.Hosting.Lifetime"))
            .Filter.ByExcluding(
                Matching.FromSource("Microsoft.AspNetCore.DataProtection.KeyManagement.XmlKeyManager")
            )
            .CreateLogger();
        services.AddSingleton(Log.Logger);
        return services;
    }

    public static IServiceCollection AddCaching(this IServiceCollection services)
    {
        services.AddMemoryCache();
        return services;
    }
        
    public static IServiceCollection AddCustomMvc(this IServiceCollection services)
    {
        //var assembly = Assembly.Load(typeof(Ambiente).Assembly.ToString());
        services
            .AddControllers(o =>
            {
                o.Filters.Add(typeof(HttpGlobalExceptionFilter));
            });
        //services.AddApplicationPart(assembly);
        return services;
    }
}