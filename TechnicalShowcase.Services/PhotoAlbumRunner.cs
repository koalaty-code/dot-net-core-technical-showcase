using System;
using System.Threading.Tasks;
using TechnicalShowcase.Services.RestClients;
using TechnicalShowcase.Services.Wrappers;

namespace TechnicalShowcase.Services
{
    public interface IPhotoAlbumRunner
    {
        Task Run();
    }

    public class PhotoAlbumRunner : IPhotoAlbumRunner
    {
        private const string Prompt = "Album ID: ";
        private readonly IPhotoAlbumClient _photoAlbumClient;
        private readonly IConsoleWrapper _consoleWrapper;

        public PhotoAlbumRunner(IPhotoAlbumClient photoAlbumClient, IConsoleWrapper consoleWrapper)
        {
            _photoAlbumClient = photoAlbumClient;
            _consoleWrapper = consoleWrapper;
        }

        public async Task Run()
        {
            _consoleWrapper.WriteLine("Welcome to the Photo Album runner. Enter an Album ID to search for photos or \"Q\" to quit.");
            _consoleWrapper.Write(Prompt);

            
            var input = _consoleWrapper.ReadLine();
            while (!string.Equals(input, "Q", StringComparison.InvariantCultureIgnoreCase))
            {
                if (!int.TryParse(input, out var albumId))
                {
                    _consoleWrapper.WriteLine("Invalid input. Please enter an integer value or \"Q\" to quit.");
                }
                else
                {
                    var photos = await _photoAlbumClient.GetPhotosByAlbum(albumId);

                    foreach (var photo in photos)
                    {
                        _consoleWrapper.WriteLine(photo);
                    }
                }

                _consoleWrapper.Write(Prompt);
                input = _consoleWrapper.ReadLine();
            }
        }
    }
}
