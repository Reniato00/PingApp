namespace PingViewerApp;

public class PingResult
{
    public string Host { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int? TimeMs { get; set; }
}
