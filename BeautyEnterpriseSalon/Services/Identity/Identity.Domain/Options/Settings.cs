namespace Identity.Domain.Options;

public class Settings
{
    public string? Secret { get; set; }
    public string? ExpiracaoHoras { get; set; }
    public string? Emisor { get; set; }
    public string? ValidoEm { get; set; }
}
