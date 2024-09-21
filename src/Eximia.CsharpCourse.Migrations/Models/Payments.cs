namespace Eximia.CsharpCourse.Migrations.Models;

public record Payments
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public Orders Order { get; set; } = null!;
    public string ExternalId { get; set; } = string.Empty;
    public int? Installments { get; set; }
    public string Method { get; set; } = string.Empty;
    public bool WasRefund { get; set; }
}
