using Eximia.CsharpCourse.Products;
using Eximia.CsharpCourse.Products.Discounts;

namespace Eximia.CsharpCourse.Orders.DomainServices.CalculateOrderValue;

public class CalculateOrderValueDomainService : ICalculateOrderValueDomainService
{
    public void Calculate(Order order, IEnumerable<Product> products)
    {
        foreach (var item in order.Items)
        {
            var product = products.FirstOrDefault(p => p.Id == item.ProductId);
            if (product is null)
                continue;

            var discount = product.CalculateDiscount(new DiscountStrategyContext(item.Quantity, order.Date, item.Amount, order.PaymentMethod.Method));
            item.ApplyDiscount(discount);
        }
    }
}
