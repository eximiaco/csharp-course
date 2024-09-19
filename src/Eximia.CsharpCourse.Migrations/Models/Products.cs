namespace Eximia.CsharpCourse.Migrations.Models;

public record Products
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public string DiscountStrategies { get; set; } = string.Empty;
}
