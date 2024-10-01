using CSharpFunctionalExtensions;
using Eximia.CsharpCourse.Payments;

namespace Eximia.CsharpCourse.Orders.States;

public interface IOrderState
{
    string Name { get; }

    Result Cancel(Order order);
    Result ProcessPayment(Order order);
    Result CompletePayment(Order order, Payment payment);
    Result Complete(Order order);
}
