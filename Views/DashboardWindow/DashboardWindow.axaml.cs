using Avalonia.Controls;
using Avalonia.Interactivity;
using PingViewerApp.Bussines.Entities;
using PingViewerApp.Bussines.Services;

namespace PingViewerApp;

public partial class DashboardWindow : Window
{
    private readonly IPingMonitor pingMonitor;
    public DashboardWindow(PingResultsViewModel vm, IPingMonitor pingMonitor)
    {
        InitializeComponent();
        var pingResultsViewModel = vm;
        this.pingMonitor = pingMonitor;
        DataContext = pingResultsViewModel;
    }

    private async void OnRepingClick(object? sender, RoutedEventArgs e) 
    {
        if (sender is Button btn && btn.DataContext is PingResult pingResult) 
        {
            var item = new PingItem
            {
                Name = pingResult.Name,
                Host = pingResult.Host
            };

            btn.IsEnabled = false;

            var result = await pingMonitor.RepingOneAsync(item);

            await Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() =>
            {
                pingResult.TimeMs = result.TimeMs;
                pingResult.Status = result.Status;
            });

            btn.IsEnabled= true;
        }
    }
}
