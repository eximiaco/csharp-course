using Eximia.CsharpCourse.Orders.Consumers;
using Eximia.CsharpCourse.Payments.Consumers;
using Eximia.CsharpCourse.SeedWork.Settings;
using Eximia.CsharpCourse.Workers.Jobs;
using Flurl.Http;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Eximia.CsharpCourse.API.Infrastructure;

internal static class ServicesExtensions
{
    internal static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<StockApiSettings>(configuration.GetSection("StockApi"));
        services.Configure<EbanxApiSettings>(configuration.GetSection("EbanxApi"));
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

    internal static IServiceCollection AddWorkersServices(this IServiceCollection services)
    {
        services.Configure<HostOptions>(x =>
        {
            x.ServicesStartConcurrently = true;
        });

        services.AddHostedService<ProcessOrdersPaymentsJob>();
        return services;
    }

    internal static IServiceCollection AddHealth(this IServiceCollection services, IConfiguration configuration)
    {
        var hcBuilder = services.AddHealthChecks();
        hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy(), new string[] { "ready" });
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

    internal static IServiceCollection AddBus(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<ProcessPaymentConsumer>();
            x.AddConsumer<CompleteOrderPaymentConsumer>();
            x.AddConsumer<RefundOrderPaymentConsumer>();

            x.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
