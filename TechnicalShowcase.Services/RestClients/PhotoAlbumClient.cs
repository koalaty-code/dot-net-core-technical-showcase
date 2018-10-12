using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TechnicalShowcase.Data.Models;
using TechnicalShowcase.Services.Wrappers;

namespace TechnicalShowcase.Services.RestClients
{
    public interface IPhotoAlbumClient
    {
        Task<List<Photo>> GetPhotosByAlbum(int albumId);
    }
    public class PhotoAlbumClient : IPhotoAlbumClient
    {
        private const string RootPhotoUri = "https://jsonplaceholder.typicode.com/photos";

        private readonly HttpClient _httpClient;
        private readonly IJsonConvertWrapper _jsonWrapper;

        public PhotoAlbumClient(HttpClient httpClient, IJsonConvertWrapper jsonWrapper)
        {
            _httpClient = httpClient;
            _jsonWrapper = jsonWrapper;
        }

        public async Task<List<Photo>> GetPhotosByAlbum(int albumId)
        {
            var response = await _httpClient.GetAsync($"{RootPhotoUri}?album={albumId}");

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("The Photo Album API cannot be reached at this time.");

            var content = await response.Content.ReadAsStringAsync();
            return _jsonWrapper.Deserialize<List<Photo>>(content);
        }
    }
}
