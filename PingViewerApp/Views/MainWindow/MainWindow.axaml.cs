using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.DependencyInjection;

namespace PingViewerApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void OnEnterButtonClick(object sender, RoutedEventArgs e)
    {
        var dashboard = App.Services.GetRequiredService<DashboardWindow>();
        dashboard.Show();
        Close();
    }
}