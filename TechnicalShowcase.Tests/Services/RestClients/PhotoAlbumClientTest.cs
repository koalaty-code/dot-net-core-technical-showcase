using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using TechnicalShowcase.Data.Models;
using TechnicalShowcase.Services.RestClients;
using TechnicalShowcase.Services.Wrappers;

namespace TechnicalShowcase.Tests.Services.RestClients
{
    [TestClass]
    public class PhotoAlbumClientTest
    {
        private PhotoAlbumClient _albumClient;
        private HttpClient _httpClient;
        private Mock<IJsonConvertWrapper> _jsonWrapperMock;
        private Mock<HttpMessageHandler> _messageHandlerMock;
        private Randomizer _random;
        private Faker<Photo> _testPhotos;

        [TestInitialize]
        public void BeforeEach()
        {
            _random = new Randomizer();
            _testPhotos = new Faker<Photo>();

            _messageHandlerMock = new Mock<HttpMessageHandler>();
            _jsonWrapperMock = new Mock<IJsonConvertWrapper>();
            _httpClient = new HttpClient(_messageHandlerMock.Object);
            _albumClient = new PhotoAlbumClient(_httpClient, _jsonWrapperMock.Object);
        }

        [TestClass]
        public class GetTest : PhotoAlbumClientTest
        {
            private const string RootUrl = "https://jsonplaceholder.typicode.com/photos";

            private int _expectedAlbumId;
            private string _expectedGetPhotosUrl;
            private HttpResponseMessage _expectedResponseMessage;
            private string _expectedResponseContent;
            private List<Photo> _expectedPhotos;

            [TestInitialize]
            public void BeforeEachGet()
            {
                _expectedAlbumId = _random.Number();
                _expectedGetPhotosUrl = $"{RootUrl}?album={_expectedAlbumId}";
                _expectedResponseContent = _random.Words();
                _expectedPhotos = _testPhotos.Generate(5);
                _expectedResponseMessage = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(_expectedResponseContent)
                };

                _messageHandlerMock.Protected()
                    .Setup<Task<HttpResponseMessage>>("SendAsync",
                        ItExpr.Is<HttpRequestMessage>(message => message.RequestUri.AbsoluteUri == _expectedGetPhotosUrl),
                        ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(_expectedResponseMessage);
                _jsonWrapperMock.Setup(json => json.Deserialize<List<Photo>>(_expectedResponseContent))
                    .Returns(_expectedPhotos);
            }

            [TestMethod]
            public async Task ShouldReturnDeserializedPhotoFromPhotoAlbumRequestWhenResponseIsOk()
            {
                var actualPhoto = await _albumClient.GetPhotosByAlbum(_expectedAlbumId);

                actualPhoto.Should().BeEquivalentTo(_expectedPhotos);
            }

            [TestMethod]
            [ExpectedException(typeof(Exception))]
            public async Task ShouldThrowExceptionWithMessageThatApiCannotBeReached()
            {
                _expectedResponseMessage.StatusCode = HttpStatusCode.BadRequest;

                await _albumClient.GetPhotosByAlbum(_expectedAlbumId);
            }
        }
    }
}
