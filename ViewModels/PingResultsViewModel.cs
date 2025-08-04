using PingViewerApp.Bussines.Services;
using System.Collections.ObjectModel;

namespace PingViewerApp
{
    public class PingResultsViewModel
    {
        public ObservableCollection<PingResult> Pings { get; set; }

        public PingResultsViewModel()
        {
            //Example data for demonstration purposes

            var monitor = new PingMonitor();
            var pingResults = monitor.GetResults();
            Pings = new ObservableCollection<PingResult>(pingResults);
        }
    }
}