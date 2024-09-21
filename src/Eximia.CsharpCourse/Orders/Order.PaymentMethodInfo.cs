using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Orders;

public partial class Order
{
    public record PaymentMethodInfo
    {
        public PaymentMethodInfo(EPaymentMethod method, int? installments, bool wasRefunded)
        {
            Method = method;
            Installments = installments;
            WasRefunded = wasRefunded;
        }

        public EPaymentMethod Method { get; }
        public int? Installments { get; }
        public bool WasRefunded { get; private set; }

        public static PaymentMethodInfo Create(EPaymentMethod method, int? installments)
            => new PaymentMethodInfo(method, installments, wasRefunded: false);

        public void Refund() => WasRefunded = true;
    }
}
