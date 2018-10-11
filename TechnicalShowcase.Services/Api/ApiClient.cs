using System;
using System.Collections.Generic;
using TechnicalShowcase.Services.Wrappers;

namespace TechnicalShowcase.Services.Api
{
    public interface IApiClient
    {
        T Get<T>(string uri) where T : class;
        IEnumerable<T> GetAll<T>(string uri) where T : class;
    }
    public class ApiClient : IApiClient
    {
        private readonly IHttpClientWrapper _httpClient;

        public ApiClient(IHttpClientWrapper httpClient)
        {
            _httpClient = httpClient;
        }

        public T Get<T>(string uri) where T : class
        {
            _httpClient.GetAsync(uri);
            return null;
        }

        public IEnumerable<T> GetAll<T>(string uri) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
