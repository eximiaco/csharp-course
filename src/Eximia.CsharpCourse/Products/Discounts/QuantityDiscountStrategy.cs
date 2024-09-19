namespace Eximia.CsharpCourse.Products.Discounts;

public class QuantityDiscountStrategy(Dictionary<int, decimal> quantityDiscount) : IDiscountStrategy
{
    public Dictionary<int, decimal> QuantityDiscount { get; } = quantityDiscount;

    public decimal Calculate(DiscountStrategyContext context)
    {
        if (QuantityDiscount.TryGetValue(context.Quantity, out var discount))
            return discount;
        return 0;
    }
}
