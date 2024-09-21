using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Products.Discounts;

public readonly record struct DiscountStrategyContext(int Quantity, DateTime OrderDate, decimal Amount, EPaymentMethod PaymentMethod);
