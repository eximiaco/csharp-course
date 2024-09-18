using MediatR;

namespace Eximia.CsharpCourse.SeedWork;

public interface ICommand<TResult> : IRequest<TResult> { }
public interface ICommand : IRequest { }