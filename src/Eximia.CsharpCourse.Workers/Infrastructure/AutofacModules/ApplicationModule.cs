using Autofac;
using Eximia.CsharpCourse.SeedWork;
using System.Reflection;
using Module = Autofac.Module;

namespace Eximia.CsharpCourse.Workers.Infrastructure.AutofacModules;

internal class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assembly = typeof(IUnitOfWork).GetTypeInfo().Assembly;

        builder
            .RegisterAssemblyTypes(assembly)
            .AsClosedTypesOf(typeof(IService<>))
            .InstancePerLifetimeScope();

        builder
            .RegisterAssemblyTypes(assembly)
            .AsClosedTypesOf(typeof(IRepository<>))
            .InstancePerLifetimeScope();
    }
}
