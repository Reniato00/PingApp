using PingViewerApp.Bussines.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PingViewerApp.Bussines.Services
{
    public class PingMonitor : IPingMonitor
    {
        private readonly IPingRequester pingRequester;

        public PingMonitor(IPingRequester pingRequester)
        {
            this.pingRequester = pingRequester;
        }

        //// Método tradicional: carga todo, espera y regresa la lista completa
        //public List<PingResult> GetResults()
        //{
        //    var json = File.ReadAllText("db.json");
        //    var pings = JsonSerializer.Deserialize<PingDatabase>(json);

        //    var pingResults = pingRequester.GetPingsHealth(
        //        pings?.Pings
        //            .Select(x => new PingItem { Name = x.Name, Host = x.Host })
        //            .ToList() ?? new List<PingItem>()
        //    ).Result;

        //    return pingResults;
        //}

        // Método nuevo: ejecuta cada ping en paralelo y llama al callback cuando termine cada uno
        public async Task PingAllAsync(Action<PingResult> onPingCompleted)
        {
            var json = File.ReadAllText("db.json");
            var pings = JsonSerializer.Deserialize<PingDatabase>(json);

            var items = pings?.Pings
                .Select(x => new PingItem { Name = x.Name, Host = x.Host })
                .ToList() ?? new List<PingItem>();

            await pingRequester.PingEachAsync(items, onPingCompleted);
        }
    }

    public interface IPingMonitor
    {
        //List<PingResult> GetResults();
        Task PingAllAsync(Action<PingResult> onPingCompleted);
    }
}
