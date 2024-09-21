using CSharpFunctionalExtensions;
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

    public IOrderState State { get; private set; } = null!;
    public IEnumerable<Item> Items => _items;
    public PaymentMethodInfo PaymentMethod { get; private set; } = null!;
    public DateTime Date { get; }
    public decimal Amount => _items.Sum(i => i.Amount);
    public string StateName => State.Name;

    public static Result<Order> Create(ICollection<Item> items, PaymentMethodInfo paymentMethod)
    {
        if (paymentMethod.Method == EPaymentMethod.CreditCard && paymentMethod.Installments > 12)
            return Result.Failure<Order>("Não é possível parcelar em mais de 12x.");

        var order = new Order(
            id: 0,
            state: new AwaitingProcessingState(),
            items,
            paymentMethod,
            date: DateTime.UtcNow);

        order.AddDomainEvent(new OrderCreatedDomainEvent(order));
        return order;
    }

    public void ChangeState(IOrderState state) => State = state;
    public Result Cancel() => State.Cancel(this);
    public Result ProcessPayment() => State.ProcessPayment(this);
    public Result CompletePayment() => State.CompletePayment(this);
    public void Refund() => PaymentMethod.Refund();
}
