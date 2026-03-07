using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SensitiveWords.Application.Services
{
    public class SensitiveWordEngineLoader : IHostedService
    {
        private readonly SensitiveWordEngine _engine;

        private readonly ILogger<SensitiveWordEngineLoader> _logger;

        public SensitiveWordEngineLoader(
            SensitiveWordEngine engine,
            ILogger<SensitiveWordEngineLoader> logger)
        {
            _engine = engine;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Loading sensitive words into Trie...");

            await _engine.ReloadAsync();

            _logger.LogInformation("Sensitive words loaded successfully.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
