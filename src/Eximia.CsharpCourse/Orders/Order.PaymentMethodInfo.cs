namespace Eximia.CsharpCourse.Orders;

public partial class Order
{
    public record PaymentMethodInfo
    {
        public PaymentMethodInfo(EPaymentMethod method, int? installments)
        {
            Method = method;
            Installments = installments;
        }

        public EPaymentMethod Method { get; }
        public int? Installments { get; }
    }
}
