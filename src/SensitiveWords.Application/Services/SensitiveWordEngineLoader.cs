using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SensitiveWords.Application.Interfaces;

namespace SensitiveWords.Application.Services
{
    public class SensitiveWordEngineLoader : IHostedService
    {
        private readonly ISensitiveWordEngine _engine;
        private readonly ILogger<SensitiveWordEngineLoader> _logger;

        public SensitiveWordEngineLoader(
            ISensitiveWordEngine engine,
            ILogger<SensitiveWordEngineLoader> logger)
        {
            _engine = engine;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting SensitiveWordEngineLoader.");

            try
            {
                _logger.LogInformation("Loading sensitive words into Trie...");

                await _engine.ReloadAsync();

                _logger.LogInformation("Sensitive word Trie loaded successfully.");
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("Sensitive word loading was cancelled.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(
                    ex,
                    "Failed to load sensitive words during application startup.");

                throw; // stop application startup
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("SensitiveWordEngineLoader stopping.");

            return Task.CompletedTask;
        }
    }
}