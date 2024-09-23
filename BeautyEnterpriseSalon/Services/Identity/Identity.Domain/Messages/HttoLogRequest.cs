namespace Identity.Domain.Messages;

public class HttpRequestLog
{
    public Guid Id { get; set; }
    public string? RequestUrl { get; set; }
    public string? RequestMethod { get; set; }
    public int? ResponseStatus { get; set; }
    public string? ResponseHeaders { get; set; }
    public string? ResponseBody { get; set; }
    public DateTime ResponseTimestamp { get; set; } = DateTime.Now.ToUniversalTime().AddHours(-3);
}