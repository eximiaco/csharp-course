namespace Eximia.CsharpCourse.Products.Discounts;

public class NoDiscountStrategy : IDiscountStrategy
{
    public decimal Calculate(DiscountStrategyContext context)
        => 0;
}
