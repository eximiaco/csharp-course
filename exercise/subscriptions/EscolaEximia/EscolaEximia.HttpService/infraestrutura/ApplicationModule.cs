using Autofac;

namespace EscolaEximia.HttpService.infraestrutura;

public class ApplicationModule: Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // builder
        //     .RegisterAssemblyTypes(typeof(Ambiente).Assembly)
        //     .AsClosedTypesOf(typeof(IService<>))
        //     .InstancePerLifetimeScope();
        //
        // builder
        //     .RegisterType<InscricoesDbContextFactory>()
        //     .As<IEfDbContextFactory<InscricoesDbContext>>()
        //     .InstancePerLifetimeScope();
        //
        // builder
        //     .RegisterType<InscricoesDbContextAccessor>()
        //     .As<IEfDbContextAccessor<InscricoesDbContext>>()
        //     .InstancePerLifetimeScope();
        //
        // builder
        //     .RegisterType<EfUnitOfWork>()
        //     .As<IUnitOfWork>()
        //     .InstancePerLifetimeScope();
        //
        // builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().InstancePerLifetimeScope();
    }
}