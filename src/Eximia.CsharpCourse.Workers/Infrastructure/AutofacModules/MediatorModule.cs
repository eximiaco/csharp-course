using Autofac;
using Eximia.CsharpCourse.SeedWork.Behaviors;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using Module = Autofac.Module;
using MediatR.Extensions.Autofac.DependencyInjection;
using Eximia.CsharpCourse.SeedWork.EFCore;

namespace Eximia.CsharpCourse.Workers.Infrastructure.AutofacModules;

internal class MediatorModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var configuration = MediatRConfigurationBuilder
            .Create(typeof(IUnitOfWork).Assembly)
            .WithAllOpenGenericHandlerTypesRegistered()
            .WithCustomPipelineBehavior(typeof(UnitOfWorkBehavior<,>))
            .WithCustomPipelineBehavior(typeof(UnitOfWorkBehaviorWithResponse<,>))
            .Build();

        builder.RegisterMediatR(configuration);
    }
}
