using Microsoft.Extensions.DependencyInjection;
using TechnicalShowcase.Services.RestClients;

namespace TechnicalShowcase.DependencyModule
{
    public static class Registrar
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IPhotoAlbumClient, PhotoAlbumClient>();
        }
    }
}
