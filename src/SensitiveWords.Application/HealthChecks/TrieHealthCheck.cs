using Microsoft.Extensions.Diagnostics.HealthChecks;
using SensitiveWords.Application.Interfaces;

namespace SensitiveWords.Application.HealthChecks
{
    public class TrieHealthCheck : IHealthCheck
    {
        private readonly ISensitiveWordEngine _engine;

        public TrieHealthCheck(ISensitiveWordEngine engine)
        {
            _engine = engine;
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            if (!_engine.IsInitialized)
            {
                return Task.FromResult(
                    HealthCheckResult.Degraded("Trie is still loading"));
            }

            return Task.FromResult(
                HealthCheckResult.Healthy("Trie loaded successfully"));
        }
    }
}
