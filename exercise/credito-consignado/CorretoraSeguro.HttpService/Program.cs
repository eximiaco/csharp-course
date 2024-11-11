using System.Reflection;
using CorretoraSeguro.HttpService.Domain.SeedWork;
using CorretoraSeguro.HttpService.Warmup;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var assemblyName = Assembly.GetExecutingAssembly().GetName();
var serviceName = assemblyName.Name;
var serviceVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString();

try
{
    Log.ForContext("ApplicationName", serviceName).Information("Starting application");
    builder.Services
        .AddEndpointsApiExplorer()
        .AddSwaggerDoc()
        .AddVersioning()
        .AddCustomCors()
        .AddSecurity(builder.Configuration)
        .AddHealth(builder.Configuration)
        .AddWorkersServices(builder.Configuration)
        .AddOptions()
        .AddCaching()
        .AddLogs(builder.Configuration, serviceName!)
        .AddCustomMvc();
    
    builder.Services.AddDbContext<PropostasDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("PropostasConnection")));
    builder.Services.AddHostedService<DatabaseInitializer>();
    
    builder.Host.UseSerilog();
    
    var app = builder.Build();
    app.UseHealthChecks("/health-ready");
    app.UseHealthChecks("/health-check");
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
    return 0;
}
catch (Exception ex)
{
    Log.ForContext("ApplicationName", serviceName)
        .Fatal(ex, "Program terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}