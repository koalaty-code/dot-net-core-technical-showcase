using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using TechnicalShowcase.Services.Wrappers;

namespace TechnicalShowcase.Services
{
    public class PhotoAlbumRunner : IHostedService
    {
        private readonly IConsoleWrapper _consoleWrapper;

        public PhotoAlbumRunner(IConsoleWrapper consoleWrapper)
        {
            _consoleWrapper = consoleWrapper;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
           throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
