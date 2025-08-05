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
            _ = StartMonitoringLoopAsync(); // Se ignora el await aquí para no cambiar a constructor async
        }

        private async Task LoadAsync()
        {
            await pingMonitor.PingAllAsync(result =>
            {
                // Esto asegura que los cambios en la colección ocurran en el hilo de UI
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
                    // Usar el dispatcher para actualizar la UI desde hilo secundario
                    Avalonia.Threading.Dispatcher.UIThread.Post(() =>
                    {
                        Pings.Add(result);
                    });
                });

                await Task.Delay(TimeSpan.FromSeconds(15)); // Esperar 15 segundos antes de repetir
            }
        }
    }
}