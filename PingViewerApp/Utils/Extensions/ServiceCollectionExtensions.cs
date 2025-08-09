using Microsoft.Extensions.DependencyInjection;
using PingViewerApp.Bussines.Services;

namespace PingViewerApp.Utils.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCommonServices(this IServiceCollection services) 
        {
            services.AddTransient<IPingMonitor, PingMonitor>();
            services.AddTransient<IPingRequester, PingRequester>();
            services.AddTransient<IFileManager, FileManager>();
        }

        public static void AddViewModels(this IServiceCollection services) 
        {
            services.AddTransient<PingResultsViewModel>();
        }

        public static void AddViews(this IServiceCollection services) 
        {
            services.AddTransient<MainWindow>();
            services.AddTransient<DashboardWindow>();
        }
    }
}
