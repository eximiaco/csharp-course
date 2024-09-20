using CSharpFunctionalExtensions;

namespace Eximia.CsharpCourse.Orders.States;

public interface IOrderState
{
    string Name { get; }

    Result Cancel(Order order);
}
