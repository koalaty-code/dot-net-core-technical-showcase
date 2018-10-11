using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using TechnicalShowcase.Services.Api;

namespace TechnicalShowcase.Tests.Services.Api
{
    [TestClass]
    public class ApiClientTest
    {
        private ApiClient _apiClient;
        private HttpClient _httpClient;
        private Mock<HttpMessageHandler> _messageHandlerMock;
        private Randomizer _random;

        [TestInitialize]
        public void BeforeEach()
        {
            _random = new Randomizer();

            _messageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_messageHandlerMock.Object);

            _apiClient = new ApiClient(_httpClient);
        }

        [TestClass]
        public class GetTest : ApiClientTest
        {
            private string _expectedUrl;
            private string _expectedResponseMessage;

            [TestInitialize]
            public void BeforeEachGet()
            {
                _expectedUrl = new Bogus.DataSets.Internet().Url();
                _expectedResponseMessage = _random.Words();

                _messageHandlerMock.Protected()
                    .Setup<Task<HttpResponseMessage>>("SendAsync", 
                        ItExpr.Is<HttpRequestMessage>(message => message.RequestUri.OriginalString == _expectedUrl),
                        ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(_expectedResponseMessage)
                    });
            }

            [TestMethod]
            public async Task ShouldCallHttpClientFactoryToCreateANewHttpClient()
            {
                var actualResponse = await _apiClient.Get<string>(_expectedUrl);

                actualResponse.Should().Be(_expectedResponseMessage);
            }
        }
    }
}
