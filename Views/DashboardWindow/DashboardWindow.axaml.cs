using Avalonia.Controls;

namespace PingViewerApp;

public partial class DashboardWindow : Window
{
    public DashboardWindow()
    {
        InitializeComponent();
        var pingResultsViewModel = new PingResultsViewModel();
        DataContext = pingResultsViewModel;

    }
}
