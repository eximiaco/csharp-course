using Autofac.Extensions.DependencyInjection;
using Autofac;
using Eximia.CsharpCourse.API.Infrastructure;
using Eximia.CsharpCourse.Workers.Infrastructure.AutofacModules;

var builder = WebApplication.CreateBuilder(args);

builder
    .Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables();

builder.Services
    .AddHealth(builder.Configuration)
    .AddCustomOptions(builder.Configuration)
    .AddBus()
    .AddWorkersServices()
    .AddControllers();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new ApplicationModule());
    containerBuilder.RegisterModule(new InfrastructureModule());
    containerBuilder.RegisterModule(new MediatorModule());
});

var app = builder.Build();

app.UseHealthChecks("/health-check");
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
