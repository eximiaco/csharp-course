using CSharpFunctionalExtensions;

namespace Eximia.CsharpCourse.Orders.States;

public interface IOrderState
{
    string Name { get; }

    Result CanCancel(Order order);
    Result CanProcessPayment(Order order);
    Result CanCompletePayment(Order order);
    Result CanSeparate(Order order);
    Result WaitForStock(Order order);
    Result CanComplete(Order order);
}
