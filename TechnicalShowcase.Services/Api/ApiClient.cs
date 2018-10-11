using System.Net.Http;
using System.Threading.Tasks;

namespace TechnicalShowcase.Services.Api
{
    public interface IApiClient
    {
        Task<T> Get<T>(string uri) where T : class;
    }
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> Get<T>(string uri) where T : class
        {
            var response = await _httpClient.GetAsync(uri);
            return await response.Content.ReadAsStringAsync() as T;
        }
    }
}
