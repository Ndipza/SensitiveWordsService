using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Registry;
using SensitiveWords.Application.Common.Policies;
using SensitiveWords.Application.Interfaces;

namespace SensitiveWords.Application.Services.Engine
{
    public class SensitiveWordEngineLoader : IHostedService
    {
        private readonly ISensitiveWordEngine _engine;
        private readonly ILogger<SensitiveWordEngineLoader> _logger;
        private readonly IAsyncPolicy _retryPolicy;

        public SensitiveWordEngineLoader(
            ISensitiveWordEngine engine,
            PolicyRegistry registry,
            ILogger<SensitiveWordEngineLoader> logger)
        {
            _engine = engine;
            _logger = logger;
            _retryPolicy = registry.Get<IAsyncPolicy>(PollyPolicies.DatabaseRetry);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting SensitiveWordEngineLoader.");

            await _retryPolicy.ExecuteAsync(async () =>
            {
                _logger.LogInformation("Loading sensitive words into Trie...");
                await _engine.ReloadAsync();
                _logger.LogInformation("Sensitive words loaded successfully.");
            });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("SensitiveWordEngineLoader stopping.");
            return Task.CompletedTask;
        }
    }
}