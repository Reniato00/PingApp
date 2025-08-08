using PingViewerApp.Bussines.Entities;
using PingViewerApp.Bussines.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PingViewerApp
{
    public class PingResultsViewModel
    {
        public ObservableCollection<PingResult> Pings { get; set; } = new();

        private readonly IPingMonitor pingMonitor;
        public PingResultsViewModel(IPingMonitor pingMonitor)
        {
            this.pingMonitor = pingMonitor;
            _ = StartMonitoringLoopAsync();
        }

        //private async Task LoadAsync()
        //{
        //    await pingMonitor.PingAllAsync(result =>
        //    {
        //        Avalonia.Threading.Dispatcher.UIThread.Post(() =>
        //        {
        //            Pings.Add(result);
        //        });
        //    });
        //}

        private async Task StartMonitoringLoopAsync()
        {
            while (true)
            {
                // Limpia los resultados anteriores si quieres que se reemplacen:
                Avalonia.Threading.Dispatcher.UIThread.Post(() => Pings.Clear());

                await pingMonitor.PingAllAsync(result =>
                {
                    Avalonia.Threading.Dispatcher.UIThread.Post(() =>
                    {
                        Pings.Add(result);
                    });
                });

                await Task.Delay(TimeSpan.FromSeconds(30)); // Esperar 30 segundos antes de repetir
            }
        }

        public void DeleteHost(PingResult pingResult)
        {
            var itemToDelete = new PingItem
            {
                Host = pingResult.Host,
                Name = pingResult.Name
            };
            pingMonitor.DeleteHost(itemToDelete);
            Pings.Remove(pingResult);
        }

        public void AddNewHost(string name, string host) 
        {
            var newItem = new PingItem
            {
                Name = name,
                Host = host
            };

            Pings.Add(new PingResult
            {
                Name = newItem.Name,
                Host = newItem.Host,
                Status = "Pending",
                TimeMs = null
            });

            pingMonitor.AddNewHost(newItem);
        }

        //private async Task RepingAsync(PingResult pingResult) 
        //{
        //    var newResult = await pingMonitor.RepingOneAsync(new PingItem
        //    {
        //        Host = pingResult.Host,
        //        Name = pingResult.Name,
        //    });

        //    Avalonia.Threading.Dispatcher.UIThread.Post(() =>
        //    {
        //        pingResult.Status = newResult.Status;
        //        pingResult.TimeMs = newResult.TimeMs;
        //    });
        //}

        public async Task RePingAllAsync()
        {
            await pingMonitor.RepingAllAsync(Pings.ToList(),null);
        }
    }
}