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

        return new Order(
            id: 0,
            state: new AwaitingProcessingState(),
            items,
            paymentMethod,
            date: DateTime.UtcNow);
    }

    public Result Cancel()
    {
        var result = State.CanCancel(this);
        if (result.IsFailure)
            return result;

        State = new CanceledState();
        return result;
    }

    public Result ProcessPayment()
    {
        var result = State.CanProcessPayment(this);
        if (result.IsFailure)
            return result;

        State = new ProcessingPaymentState();
        AddDomainEvent(new OrderIsProcessingPaymentDomainEvent(this));
        return result;
    }

    public Result CompletePayment()
    {
        var result = State.CanCompletePayment(this);
        if (result.IsFailure)
            return result;

        State = new PaymentCompletedState();
        return result;
    }

    public Result Complete()
    {
        var result = State.CanComplete(this);
        if (result.IsFailure)
            return result;

        State = new CompletedState();
        return result;
    }

    public Result Separate()
    {
        var result = State.CanSeparate(this);
        if (result.IsFailure)
            return result;

        State = new SeparatingOrderState();
        AddDomainEvent(new OrderIsBeingSeparatedDomainEvent(this));
        return result;
    }

    public Result WaitForStock()
    {
        var result = State.WaitForStock(this);
        if (result.IsFailure)
            return result;

        State = new AwaitingForStockState();
        return result;
    }

    public void Refund() => PaymentMethod.Refund();
}
