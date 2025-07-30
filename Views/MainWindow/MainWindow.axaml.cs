using Avalonia.Controls;
using Avalonia.Interactivity;

namespace PingViewerApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void OnEnterButtonClick(object sender, RoutedEventArgs e)
    {
        var dashboard = new DashboardWindow();
        dashboard.Show();
        Close();
    }
}