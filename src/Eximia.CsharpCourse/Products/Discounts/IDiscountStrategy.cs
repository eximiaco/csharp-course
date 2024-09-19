namespace Eximia.CsharpCourse.Products.Discounts;

public interface IDiscountStrategy
{
    decimal Calculate(DiscountStrategyContext context);
}
