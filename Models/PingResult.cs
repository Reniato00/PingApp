namespace PingViewerApp;

public class PingResult
{
    public string Name { get; set; } = string.Empty;
    public string Host { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int? TimeMs { get; set; }
}
