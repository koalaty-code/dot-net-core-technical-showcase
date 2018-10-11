using Microsoft.Extensions.DependencyInjection;
using TechnicalShowcase.Services.Api;

namespace TechnicalShowcase.DependencyModule
{
    public static class Registrar
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IApiClient, ApiClient>();
        }
    }
}
