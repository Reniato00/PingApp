namespace PingViewerApp;

public class PingResult
{
    public required string Host { get; set; }
    public required string Status { get; set; }
    public int? TimeMs { get; set; }
}
