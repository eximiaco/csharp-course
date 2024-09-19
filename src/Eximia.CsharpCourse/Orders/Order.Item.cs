using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Orders;

public partial class Order
{
    public class Item : Entity<int>
    {
        private Item() { }

        public Item(decimal amount, int quantity, int productId)
        {
            Amount = amount;
            Quantity = quantity;
            ProductId = productId;
        }

        public decimal Amount { get; }
        public int Quantity { get; }
        public int ProductId { get; }
    }
}
