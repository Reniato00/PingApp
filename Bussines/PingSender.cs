using PingViewerApp.Bussines.Entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace PingViewerApp.Bussines
{
    public class PingSender
    {
        public async Task<List<PingResult>> GetResults()
        {
            var json = File.ReadAllText("db.json");
            var pings = JsonSerializer.Deserialize<PingDatabase>(json);

            return new List<PingResult>();
        }
    }
}
