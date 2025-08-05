using Avalonia.Controls;

namespace PingViewerApp;

public partial class DashboardWindow : Window
{
    public DashboardWindow(PingResultsViewModel vm)
    {
        InitializeComponent();
        var pingResultsViewModel = vm;
        DataContext = pingResultsViewModel;
    }
}
