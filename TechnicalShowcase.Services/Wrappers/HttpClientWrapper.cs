using System.Net.Http;
using System.Threading.Tasks;

namespace TechnicalShowcase.Services.Wrappers
{
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> GetAsync(string uri);
    }

    public class HttpClientWrapper : IHttpClientWrapper
    {
        public Task<HttpResponseMessage> GetAsync(string uri)
        {
            using (var client = new HttpClient())
            {
                return client.GetAsync(uri);
            }
        }
    }
}
