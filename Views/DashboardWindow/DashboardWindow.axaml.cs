using Avalonia.Controls;
using Avalonia.Interactivity;
using PingViewerApp.Bussines.Entities;
using PingViewerApp.Bussines.Services;

namespace PingViewerApp;

public partial class DashboardWindow : Window
{
    private readonly PingResultsViewModel vm;
    private readonly IPingMonitor pingMonitor;
    public DashboardWindow(PingResultsViewModel vm, IPingMonitor pingMonitor)
    {
        InitializeComponent();
        this.vm = vm;
        this.pingMonitor = pingMonitor;
        DataContext = this.vm;
    }

    private async void OnRepingClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.DataContext is PingResult pingResult)
        {

            btn.IsEnabled = false;

            await vm.RepingClick(pingResult);

            btn.IsEnabled = true;
        }
    }

    private async void OnRepingAll(object? sender, RoutedEventArgs e)
    {
        if (DataContext is PingResultsViewModel vm)
        {
            await vm.RePingAllAsync();
        }
    }

    private void OnAddNewHost(object? sender, RoutedEventArgs e)
    {
        var name = NameTxtBox.Text;
        var host = HostTxtBox.Text;

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(host))
        {
            return;
        }

        if (DataContext is PingResultsViewModel vm)
        {
            vm.AddNewHost(name, host);
        }

        NameTxtBox.Text = string.Empty;
        HostTxtBox.Text = string.Empty;
    }

    private void OnDeleteHost(object? sender, RoutedEventArgs e) 
    {
        if (sender is Button btn && btn.DataContext is PingResult pingResult) 
        {
            if (DataContext is PingResultsViewModel vm) 
            {
                vm.DeleteHost(pingResult);
            }
        }
    }
}