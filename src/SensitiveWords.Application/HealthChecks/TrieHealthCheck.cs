using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using SensitiveWords.Application.Interfaces;

namespace SensitiveWords.Application.HealthChecks
{
    public class TrieHealthCheck : IHealthCheck
    {
        private readonly ISensitiveWordEngine _engine;
        private readonly ILogger<TrieHealthCheck> _logger;


        public TrieHealthCheck(ISensitiveWordEngine engine, ILogger<TrieHealthCheck> logger)
        {
            _engine = engine;
            _logger = logger;
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            if (!_engine.IsInitialized)
            {
                _logger.LogWarning("Trie is not initialized yet.");
                return Task.FromResult(
                    HealthCheckResult.Degraded("Trie is still loading"));
            }

            return Task.FromResult(
                HealthCheckResult.Healthy("Trie loaded successfully"));
        }
    }
}
