using System.Net.Http;
using System.Threading.Tasks;

namespace TechnicalShowcase.Services.RestClients
{
    public interface IPhotoAlbumClient
    {
        Task<T> Get<T>(string uri) where T : class;
    }
    public class PhotoAlbumClient : IPhotoAlbumClient
    {
        private readonly HttpClient _httpClient;

        public PhotoAlbumClient(HttpClient httpClient)
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
