namespace Eximia.CsharpCourse.Orders.Status;

public interface IOrderStatus
{
    string Name { get; }
    void Handle(Order order);
}
