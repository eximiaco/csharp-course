namespace Eximia.CsharpCourse.Products.Discounts;

public class SeasonalDiscountStrategy : IDiscountStrategy
{
    public SeasonalDiscountStrategy(Dictionary<SeasonalDiscountStrategyPeriodDto, decimal> seasonalDiscount)
    {
        SeasonalDiscount = seasonalDiscount;
    }

    public Dictionary<SeasonalDiscountStrategyPeriodDto, decimal> SeasonalDiscount { get; }

    public decimal Calculate(DiscountStrategyContext context)
    {
        foreach (var item in SeasonalDiscount)
        {
            if (item.Key.IsInPeriod(context.OrderDate))
                return item.Value;
        }

        return 0;
    }

    public readonly record struct SeasonalDiscountStrategyPeriodDto(DateTime StartDate, DateTime EndDate)
    {
        public bool IsInPeriod(DateTime dateTime)
            => dateTime >= StartDate && dateTime <= EndDate;
    }
}
