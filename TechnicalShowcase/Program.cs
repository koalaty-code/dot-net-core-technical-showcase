using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TechnicalShowcase.DependencyModule;
using TechnicalShowcase.Services;

namespace TechnicalShowcase
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            services.RegisterServices();

            var serviceProvider = services.BuildServiceProvider();
            var runner = serviceProvider.GetService<IPhotoAlbumRunner>();

            try
            {
                await runner.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write("Press any key to quit.");
                Console.ReadLine();
            }
        }
    }
}
