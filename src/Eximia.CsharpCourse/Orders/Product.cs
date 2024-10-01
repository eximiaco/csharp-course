using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Orders;

public class Product : AggregateRoot<int>
{
    private readonly ICollection<IDiscountStrategy> _discountStrategies;

    private Product()
    {
        _discountStrategies = [];
    }

    public Product(int id, ICollection<IDiscountStrategy> discountStrategies, string description) : base(id)
    {
        _discountStrategies = discountStrategies ?? [];
        Description = description;
    }

    public string Description { get; } = string.Empty;
    public IEnumerable<IDiscountStrategy> DiscountStrategies => _discountStrategies;

    public decimal CalculateDiscount(DiscountStrategyContext context)
    {
        decimal discount = 0;
        foreach (var discountStrategy in _discountStrategies)
            discount += discountStrategy.Calculate(context);
        return discount;
    }
}

public interface IDiscountStrategy
{
    decimal Calculate(DiscountStrategyContext context);
}

public readonly record struct DiscountStrategyContext(int Quantity, DateTime OrderDate, decimal Amount, EPaymentMethod PaymentMethod);