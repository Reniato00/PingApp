using PingViewerApp.Bussines.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PingViewerApp.Bussines.Services
{
    public class PingMonitor
    {
        public async Task<List<PingResult>> GetResults()
        {
            var json = File.ReadAllText("db.json");
            var pings = JsonSerializer.Deserialize<PingDatabase>(json);
            var pingRequester = new PingRequester();
            var pingResults = await pingRequester.GetPingsHealth(pings?.Pings
                .Select(x => new PingItem { Name = x.Name, Host = x.Host })
                .ToList() ?? new List<PingItem>());
            return pingResults;
        }
    }
}
