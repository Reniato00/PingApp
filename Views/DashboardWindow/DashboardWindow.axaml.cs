using Avalonia.Controls;
using System.Collections.ObjectModel;

namespace PingViewerApp;

public partial class DashboardWindow : Window
{
    public DashboardWindow()
    {
        InitializeComponent();

        // Este es el truco: datos directamente aquí
        var datos = new ObservableCollection<PingResult>
        {
            new() { Host = "google.com", Status = "Success", TimeMs = 45 },
            new() { Host = "github.com", Status = "Timeout", TimeMs = null },
            new() { Host = "openai.com", Status = "Success", TimeMs = 31 }
        };

        PingDataGrid.ItemsSource = datos;
    }
}
