using System.Collections.Generic;
using System.Threading.Tasks;
using Bogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TechnicalShowcase.Data.Models;
using TechnicalShowcase.Services;
using TechnicalShowcase.Services.RestClients;
using TechnicalShowcase.Services.Wrappers;

namespace TechnicalShowcase.Tests.Services
{
    [TestClass]
    public class PhotoAlbumRunnerTest
    {
        private PhotoAlbumRunner _runner;
        private Mock<IPhotoAlbumClient> _photoClientMock;
        private Mock<IConsoleWrapper> _consoleWrapperMock;
        private Randomizer _random;
        private Faker<Photo> _testPhotos;

        [TestInitialize]
        public void BeforeEach()
        {
            _random = new Randomizer();
            _testPhotos = new Faker<Photo>();

            _photoClientMock = new Mock<IPhotoAlbumClient>();
            _consoleWrapperMock = new Mock<IConsoleWrapper>();
            _runner = new PhotoAlbumRunner(_photoClientMock.Object, _consoleWrapperMock.Object);
        }

        [TestClass]
        public class RunTest : PhotoAlbumRunnerTest
        {
            private int _expectedAlbumId;
            private List<Photo> _expectedPhotos;
            private Queue<string> _expectedReadLineQueue;

            [TestInitialize]
            public void BeforeEachRun()
            {
                _expectedAlbumId = _random.Number(101, 200);
                _expectedPhotos = _testPhotos.Generate(_random.Number(1, 10));
                _expectedReadLineQueue = _populateReadLineQueue();

                _consoleWrapperMock.Setup(con => con.ReadLine()).Returns(() => _expectedReadLineQueue.Dequeue());
                _photoClientMock.Setup(client => client.GetPhotosByAlbum(It.IsAny<int>()))
                    .ReturnsAsync(new List<Photo>());
                _photoClientMock.Setup(client => client.GetPhotosByAlbum(_expectedAlbumId))
                    .ReturnsAsync(_expectedPhotos);
            }

            [TestMethod]
            public async Task ShouldWriteWelcomeMessageToConsoleWhenRunCalled()
            {
                await _runner.Run();

                _consoleWrapperMock.Verify(con =>
                    con.WriteLine(
                        "Welcome to the Photo Album runner. Enter an Album ID to search for photos or \"Q\" to quit."));
            }

            [TestMethod]
            public async Task ShouldWritePromptForAlbumIdToConsoleUntilQuitCommandRead()
            {
                var expectedCallCount = _expectedReadLineQueue.Count;

                await _runner.Run();

                _consoleWrapperMock.Verify(con => con.Write("Album ID: "), Times.Exactly(expectedCallCount));
            }

            [TestMethod]
            public async Task ShouldWriteInvalidInputMessageIfReadLineReturnsInvalidIntegerAndNotQuit()
            {
                _expectedReadLineQueue = new Queue<string>(new[] { _random.Word(), "Q" });

                await _runner.Run();

                _consoleWrapperMock.Verify(con =>
                    con.WriteLine("Invalid input. Please enter an integer value or \"Q\" to quit."));
            }

            [TestMethod]
            public async Task ShouldWriteEachPhotoToConsoleWhenValidIntegerRead()
            {
                await _runner.Run();

                _expectedPhotos.ForEach(photo => _consoleWrapperMock.Verify(con => con.WriteLine(photo)));
            }

            [TestMethod]
            public async Task ShouldWriteNoPhotosFoundMessageIfAlbumIdReturnsNoPhotos()
            {
                var expectedAlbumId = _random.Number().ToString();
                _expectedReadLineQueue = new Queue<string>(new[] { expectedAlbumId, "Q" });

                await _runner.Run();

                _consoleWrapperMock.Verify(con => con.WriteLine($"No photos found for albumId={expectedAlbumId}"));
            }

            [TestMethod]
            public async Task ShouldReadLineUntilQuitCharacterIsRead()
            {
                var expectedCallCount = _expectedReadLineQueue.Count;

                await _runner.Run();

                _consoleWrapperMock.Verify(con => con.ReadLine(), Times.Exactly(expectedCallCount));
            }

            [TestMethod]
            public async Task ShouldIgnoreCaseOfQuitCommand()
            {
                _expectedReadLineQueue = new Queue<string>(new[] { "q" });

                await _runner.Run();

                _consoleWrapperMock.Verify(con => con.ReadLine(), Times.Once);
            }

            private Queue<string> _populateReadLineQueue()
            {
                var queue = new Queue<string>();

                for (var i = 0; i < _random.Number(10); i++)
                    queue.Enqueue(_random.Number(100).ToString());

                queue.Enqueue(_expectedAlbumId.ToString());
                queue.Enqueue("Q");
                return queue;
            }
        }
    }
}
