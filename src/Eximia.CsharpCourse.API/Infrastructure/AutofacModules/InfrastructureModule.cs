using Autofac;
using Eximia.CsharpCourse.SeedWork.EFCore;
using Module = Autofac.Module;

namespace Eximia.CsharpCourse.API.Infrastructure.AutofacModules;

internal class InfrastructureModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<EximiaCsharpCourseDbContextFactory>().As<IEFDbContextFactory<EximiaCsharpCourseDbContext>>().InstancePerLifetimeScope();
        builder.RegisterType<EximiaCsharpCourseDbContextAccessor>().As<IEFDbContextAccessor<EximiaCsharpCourseDbContext>>().InstancePerLifetimeScope();

        builder
            .RegisterType<EfUnitOfWork>()
            .As<IEfUnitOfWork>()
            .InstancePerLifetimeScope();
    }
}
