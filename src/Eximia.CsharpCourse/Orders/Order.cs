using Eximia.CsharpCourse.Orders.Status;
using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Orders;

public partial class Order : AggregateRoot<int>
{
    private readonly ICollection<Item> _items = [];

    private Order() { }

    public Order(
        int id,
        IOrderStatus status,
        ICollection<Item> items,
        PaymentMethodInfo paymentMethod) : base(id)
    {
        Status = status;
        _items = items ?? [];
        PaymentMethod = paymentMethod;
    }

    public IOrderStatus Status { get; } = null!;
    public IEnumerable<Item> Items => _items;
    public PaymentMethodInfo PaymentMethod { get; } = null!;
}
