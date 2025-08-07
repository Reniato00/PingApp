using PingViewerApp.Bussines.Entities;
using PingViewerApp.Bussines.Services;
using System;
using System.Collections.ObjectModel;
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
            _ = StartMonitoringLoopAsync(); // Se ignora el await aqu� para no cambiar a constructor async
        }

        private async Task LoadAsync()
        {
            await pingMonitor.PingAllAsync(result =>
            {
                Avalonia.Threading.Dispatcher.UIThread.Post(() =>
                {
                    Pings.Add(result);
                });
            });
        }

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

        private async Task RepingAsync(PingResult pingResult) 
        {
            var newResult = await pingMonitor.RepingOneAsync(new PingItem
            {
                Host = pingResult.Host,
                Name = pingResult.Name,
            });

            Avalonia.Threading.Dispatcher.UIThread.Post(() =>
            {
                pingResult.Status = newResult.Status;
                pingResult.TimeMs = newResult.TimeMs;
            });
        }
    }
}