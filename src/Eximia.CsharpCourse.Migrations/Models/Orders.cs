namespace Eximia.CsharpCourse.Migrations.Models;

public record Orders
{
    public int Id { get; set; }
    public string Status { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public int? PaymentMethodInstallments { get; set; }
    public bool PaymentMethodWasRefunded { get; set; }
    public DateTime Date { get; set; }
}
