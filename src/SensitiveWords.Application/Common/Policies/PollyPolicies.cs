using Microsoft.Extensions.Logging;
using Polly;
using Polly.Registry;

namespace SensitiveWords.Application.Common.Policies
{
    public static class PollyPolicies
    {
        public const string DatabaseRetry = "DatabaseRetry";

        public static PolicyRegistry CreateRegistry(ILogger logger)
        {
            var registry = new PolicyRegistry();

            var retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(
                    retryCount: 4,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(5),
                    onRetry: (exception, delay, retryCount, context) =>
                    {
                        logger.LogWarning(
                            exception,
                            "Database retry {Retry}/4 after {Delay}s",
                            retryCount,
                            delay.TotalSeconds);
                    });

            registry.Add(DatabaseRetry, retryPolicy);

            return registry;
        }
    }
}