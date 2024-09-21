using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Products.Discounts;

public class PixPaymentMethodDiscountStrategy : IDiscountStrategy
{
    public PixPaymentMethodDiscountStrategy(decimal discountPercentage)
    {
        DiscountPercentage = discountPercentage;
    }

    public decimal DiscountPercentage { get; }

    public decimal Calculate(DiscountStrategyContext context)
    {
        if (context.PaymentMethod != EPaymentMethod.Pix)
            return 0;
        return DiscountPercentage * context.Amount / 100;
    }
}
