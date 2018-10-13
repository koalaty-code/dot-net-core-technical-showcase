using Microsoft.Extensions.DependencyInjection;
using TechnicalShowcase.Services;
using TechnicalShowcase.Services.RestClients;
using TechnicalShowcase.Services.Wrappers;

namespace TechnicalShowcase.DependencyModule
{
    public static class Registrar
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IPhotoAlbumRunner, PhotoAlbumRunner>();
            services.AddHttpClient<IPhotoAlbumClient, PhotoAlbumClient>();

            services.AddSingleton<IJsonConvertWrapper, JsonConvertWrapper>();
            services.AddSingleton<IConsoleWrapper, ConsoleWrapper>();
        }
    }
}
