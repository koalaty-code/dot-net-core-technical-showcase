using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using TechnicalShowcase.DependencyModule;

namespace TechnicalShowcase
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.RegisterServices();
                });

            try
            {
                await host.RunConsoleAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
