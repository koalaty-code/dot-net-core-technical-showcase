using Bogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TechnicalShowcase.Services.Api;
using TechnicalShowcase.Services.Wrappers;

namespace TechnicalShowcase.Tests.Services.Api
{
    [TestClass]
    public class ApiClientTest
    {
        private ApiClient _apiClient;
        private Mock<IHttpClientWrapper> _httpClientMock;
        private Randomizer _random;

        [TestInitialize]
        public void BeforeEach()
        {
            _random = new Randomizer();

            _httpClientMock = new Mock<IHttpClientWrapper>();
            _apiClient = new ApiClient(_httpClientMock.Object);
        }

        [TestClass]
        public class GetTest : ApiClientTest
        {
            [TestMethod]
            public void ShouldCallHttpClientWrapperWithUriPassedAsParameter()
            {
                var expectedUri = _random.Word();

                _apiClient.Get<object>(expectedUri);

                _httpClientMock.Verify(client => client.GetAsync(expectedUri), Times.Once);
            }
        }
    }
}
