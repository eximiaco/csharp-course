namespace Eximia.CsharpCourse.Migrations.Models;

public class OrderItems
{
    public int Id { get; set; }
    public Orders Order { get; set; } = null!;
    public decimal Amount { get; set; }
    public int Quantity { get; set; }
    public Products Product { get; set; } = null!;
}
