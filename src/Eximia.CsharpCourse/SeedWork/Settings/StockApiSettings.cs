namespace Eximia.CsharpCourse.SeedWork.Settings;

public record StockApiSettings
{
    public string ApiKey { get; set; } = string.Empty;
    public string Uri { get; set; } = string.Empty;
    public string WriteOffUri { get; set; } = string.Empty;
}
