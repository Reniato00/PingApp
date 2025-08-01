using Avalonia.Controls;
using System.Collections.ObjectModel;

namespace PingViewerApp;

public partial class DashboardWindow : Window
{
    public ObservableCollection<PingResult> Pings { get; set; }
    public DashboardWindow()
    {
        InitializeComponent();

       Pings = new ObservableCollection<PingResult>
       {
            new() { Host = "www.google.com", Status = "Success", TimeMs = 45 },
            new() { Host = "github.com", Status = "Timeout", TimeMs = null },
            new() { Host = "openai.com", Status = "Success", TimeMs = 31 },
            new() { Host = "openai.com", Status = "Success", TimeMs = 31 },
            new() { Host = "openai.com", Status = "Success", TimeMs = 31 },
            new() { Host = "openai.com", Status = "Success", TimeMs = 31 },
            new() { Host = "openai.com", Status = "Success", TimeMs = 31 },
            new() { Host = "openai.com", Status = "Success", TimeMs = 31 },
            new() { Host = "openai.com", Status = "Success", TimeMs = 31 },
            new() { Host = "openai.com", Status = "Success", TimeMs = 31 },
            new() { Host = "openai.com", Status = "Success", TimeMs = 31 },
            new() { Host = "openai.com", Status = "Success", TimeMs = 31 },
            new() { Host = "openai.com", Status = "Success", TimeMs = 31 },
            new() { Host = "openai.com", Status = "Success", TimeMs = 31 },
            new() { Host = "openai.com", Status = "Success", TimeMs = 31 },
            new() { Host = "openai.com", Status = "Success", TimeMs = 31 },
            new() { Host = "openai.com", Status = "Success", TimeMs = 31 },
            new() { Host = "openai.com", Status = "Success", TimeMs = 31 },
       };

       DataContext = this;

    }
}
