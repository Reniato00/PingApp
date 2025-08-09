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
    public interface IPingMonitor
    {
        Task<PingResult> RepingOneAsync(PingItem item);
        Task PingAllAsync(Action<PingResult> onPingCompleted);
        Task RepingAllAsync(List<PingResult> existingResults, Action<PingResult> onPingCompleted);
        void AddNewHost(PingItem newItem);
        void DeleteHost(PingItem itemToDelete);
    }

    public class PingMonitor : IPingMonitor
    {
        private readonly IPingRequester pingRequester;
        private readonly IFileManager fileManager;

        public PingMonitor(IPingRequester pingRequester, IFileManager fileManager)
        {
            this.pingRequester = pingRequester;
            this.fileManager = fileManager;
        }

        public async Task PingAllAsync(Action<PingResult> onPingCompleted)
        {
            var pings = fileManager.ExtractPings();

            var items = pings?.Pings
                .Select(x => new PingItem { Name = x.Name, Host = x.Host })
                .ToList() ?? new List<PingItem>();

            await pingRequester.PingEachAsync(items, onPingCompleted);
        }

        public void AddNewHost(PingItem newItem) 
        {
            var pings = fileManager.ExtractPings();

            if(!pings!.Pings.Any(p=>p.Host == newItem.Host))
                pings.Pings.Add(newItem);

            fileManager.UpdatePings(pings);
        }

        public void DeleteHost(PingItem itemToDelete)
        {
            var pings = fileManager.ExtractPings();

            if (pings != null) 
            {
                var item = pings.Pings.FirstOrDefault(p => p.Host == itemToDelete.Host);

                if (item != null)
                {
                    pings.Pings.Remove(item);
                    fileManager.UpdatePings(pings);
                }
            }
        }

        public async Task<PingResult> RepingOneAsync(PingItem item) => await pingRequester.PingAsync(item);

        public async Task RepingAllAsync(List<PingResult> existingResults, Action<PingResult> onPingCompleted)
        {
            var pings = fileManager.ExtractPings();

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


}
