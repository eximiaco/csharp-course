using Eximia.CsharpCourse.Orders.DomainEvents;
using Eximia.CsharpCourse.Orders.States;
using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Orders;

public partial class Order : AggregateRoot<int>
{
    private readonly ICollection<Item> _items = [];

    private Order() { }

    public Order(
        int id,
        IOrderState state,
        ICollection<Item> items,
        PaymentMethodInfo paymentMethod,
        DateTime date) : base(id)
    {
        State = state;
        _items = items ?? [];
        PaymentMethod = paymentMethod;
        Date = date;
    }

    public IOrderState State { get; } = null!;
    public IEnumerable<Item> Items => _items;
    public PaymentMethodInfo PaymentMethod { get; } = null!;
    public DateTime Date { get; }
    public decimal Amount => _items.Sum(i => i.Amount); 

    public static Order Create(ICollection<Item> items, PaymentMethodInfo paymentMethod)
    {
        var order = new Order(
            id: 0,
            state: new AwaitingProcessingState(),
            items,
            paymentMethod,
            date: DateTime.UtcNow);

        order.AddDomainEvent(new OrderCreatedDomainEvent(order));
        return order;
    }
}
