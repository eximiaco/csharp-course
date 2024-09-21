namespace Eximia.CsharpCourse.SeedWork.Settings;

public record EbanxApiSettings
{
    public string ApiKey { get; set; } = string.Empty;
    public string CreditCardUri { get; set; } = string.Empty;
    public string PixUri { get; set; } = string.Empty;
}
