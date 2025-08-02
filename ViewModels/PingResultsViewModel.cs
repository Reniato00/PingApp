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
            Pings = new ObservableCollection<PingResult>
            {
                new() { Name = "Google", Host = "google.com", Status = "Success", TimeMs = 32 },
                new() { Name = "Github", Host = "github.com", Status = "Timeout", TimeMs = 0 },
                new() { Name = "OpenAi", Host = "openai.com", Status = "Success", TimeMs = 27 },
            };
        }
    }
}