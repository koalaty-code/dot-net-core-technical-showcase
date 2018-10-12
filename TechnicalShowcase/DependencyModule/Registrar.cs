using System;
using Microsoft.Extensions.DependencyInjection;
using TechnicalShowcase.Services.RestClients;
using TechnicalShowcase.Services.Wrappers;

namespace TechnicalShowcase.DependencyModule
{
    public static class Registrar
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IPhotoAlbumClient, PhotoAlbumClient>();
            services.AddScoped<IJsonConvertWrapper, JsonConvertWrapper>();
        }
    }
}
