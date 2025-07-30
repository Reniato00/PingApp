using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PingViewerApp
{
    public class PingResultsViewModel
    {
        public ObservableCollection<PingResult> PingResults { get; set; }

        public PingResultsViewModel()
        {
            PingResults = new ObservableCollection<PingResult>
            {
                new() { Host = "google.com", Status = "Success", TimeMs = 32 },
                new() { Host = "github.com", Status = "Timeout", TimeMs = 0 },
                new() { Host = "openai.com", Status = "Success", TimeMs = 27 },
            };
        }
    }
}