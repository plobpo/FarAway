using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Orleans;

namespace FarAway.Scripts.core.Network
{
    public static class SiloController
    {
        public static async Task StartSiloAsync()
        {
            var builder = Host.CreateDefaultBuilder()
        .ConfigureServices(services =>
        {
            services.AddLogging(logging => 
            {
                logging.ClearProviders();
                logging.AddFile(@"C:\dev\SiloLog.log");
            });
        })
        .UseOrleans((siloBuilder) =>
        {
            siloBuilder
                .UseLocalhostClustering()
                .ConfigureLogging(logging => logging.AddConsole())
                .UseDashboard(options =>
                {
                    options.HostSelf = true; // Enable the dashboard to host itself
                    options.HideTrace = true; // Hide trace details in the dashboard
                    options.Port = 6666; // Set the port for the dashboard
                });
        })
        .UseConsoleLifetime();

        using var host = builder.Build();

        await host.RunAsync();
        }
    }
}