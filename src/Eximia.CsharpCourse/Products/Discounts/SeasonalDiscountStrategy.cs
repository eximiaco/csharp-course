namespace Eximia.CsharpCourse.Products.Discounts;

public class SeasonalDiscountStrategy : IDiscountStrategy
{
    public SeasonalDiscountStrategy(Dictionary<DateTime, decimal> seasonalDiscount)
    {
        SeasonalDiscount = seasonalDiscount;
    }

    public Dictionary<DateTime, decimal> SeasonalDiscount { get; }

    public decimal Calculate(DiscountStrategyContext context)
    {
        foreach (var item in SeasonalDiscount)
        {
            if (context.OrderDate <= item.Key)
                return item.Value * context.Amount / 100;
        }

        return 0;
    }
}
