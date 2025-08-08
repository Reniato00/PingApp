using PingViewerApp.Bussines.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public async Task PingAllAsync(Action<PingResult> onPingCompleted)
        {
            var json = File.ReadAllText("db.json");
            var pings = JsonSerializer.Deserialize<PingDatabase>(json);

            var items = pings?.Pings
                .Select(x => new PingItem { Name = x.Name, Host = x.Host })
                .ToList() ?? new List<PingItem>();

            await pingRequester.PingEachAsync(items, onPingCompleted);
        }

        public void AddNewHost(PingItem newItem) 
        {
            var json = File.ReadAllText("db.json");
            var pings = JsonSerializer.Deserialize<PingDatabase>(json);

            if(!pings.Pings.Any(p=>p.Host == newItem.Host))
                pings.Pings.Add(newItem);

            var updatedJson = JsonSerializer.Serialize(pings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("db.json", updatedJson);
        }

        public void DeleteHost(PingItem itemToDelete)
        {
            var json = File.ReadAllText("db.json");
            var pings = JsonSerializer.Deserialize<PingDatabase>(json);

            // Buscar el item y eliminarlo
            var item = pings.Pings.FirstOrDefault(p => p.Host == itemToDelete.Host);
            if (item != null)
            {
                pings.Pings.Remove(item);

                // Guardar los cambios en el archivo JSON
                var updatedJson = JsonSerializer.Serialize(pings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText("db.json", updatedJson);
            }
        }

        public async Task<PingResult> RepingOneAsync(PingItem item)
        {
            return await pingRequester.PingAsync(item);
        }

        public async Task RepingAllAsync(List<PingResult> existingResults, Action<PingResult> onPingCompleted)
        {
            var json = File.ReadAllText("db.json");
            var pings = JsonSerializer.Deserialize<PingDatabase>(json);

            var items = pings?.Pings
                .Select(x => new PingItem { Name = x.Name, Host = x.Host })
                .ToList() ?? new List<PingItem>();

            await pingRequester.PingEachAsync(items, result =>
            {
                var existing = existingResults.FirstOrDefault(r => r.Host == result.Host);
                if (existing != null)
                {
                    existing.Status = result.Status;
                    existing.TimeMs = result.TimeMs;
                }
                onPingCompleted?.Invoke(result);
            });
        }
    }

    public interface IPingMonitor
    {
        Task<PingResult> RepingOneAsync(PingItem item);
        Task PingAllAsync(Action<PingResult> onPingCompleted);
        Task RepingAllAsync(List<PingResult> existingResults, Action<PingResult> onPingCompleted);
        void AddNewHost(PingItem newItem);
        void DeleteHost(PingItem itemToDelete);
    }
}
