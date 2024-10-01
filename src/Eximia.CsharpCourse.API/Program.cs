using Autofac.Extensions.DependencyInjection;
using Autofac;
using Eximia.CsharpCourse.API.Infrastructure;
using Eximia.CsharpCourse.API.Infrastructure.AutofacModules;

var builder = WebApplication.CreateBuilder(args);

builder
    .Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables();

builder.Services
    .AddCustomOptions(builder.Configuration)
    .ConfigureFlurl()
    .AddHealth(builder.Configuration)
    .RemoveModelValidation()
    .AddSwagger()
    .AddControllersWithFilter();
    
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new ApplicationModule());
    containerBuilder.RegisterModule(new InfrastructureModule());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHealthChecks("/health-check");
app.UseHttpsRedirection();
app.UseUnitOfWork();
app.MapControllers();
app.Run();

public partial class Program
{
}