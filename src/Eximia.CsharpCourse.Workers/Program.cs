using Autofac.Extensions.DependencyInjection;
using Autofac;
using Eximia.CsharpCourse.Workers.Infrastructure;
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
    .AddWorkersServices()
    .AddControllers();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new ApplicationModule());
    containerBuilder.RegisterModule(new InfrastructureModule());
});

var app = builder.Build();

app.UseHealthChecks("/health-check");
app.UseHttpsRedirection();
app.UseUnitOfWork();
app.MapControllers();
app.Run();
