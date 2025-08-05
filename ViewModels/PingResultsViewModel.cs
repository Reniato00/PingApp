using PingViewerApp.Bussines.Services;
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
            _ = LoadAsync(); // Se ignora el await aquí para no cambiar a constructor async
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
    }
}