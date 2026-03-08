using SensitiveWords.Application.HealthChecks;

namespace SensitiveWords.Api.Configuration
{
    public static class HealthChecksConfiguration
    {
        public static IServiceCollection AddHealthChecksConfiguration(
            this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            var healthChecks = services.AddHealthChecks();

            // Application health check
            healthChecks.AddCheck<TrieHealthCheck>("trie");

            // Database health check (skip for tests)
            if (!environment.IsEnvironment("Testing"))
            {
                healthChecks.AddSqlServer(
                    configuration.GetConnectionString("DefaultConnection")!,
                    name: "sqlserver",
                    tags: new[] { "db", "sql" });
            }

            return services;
        }
    }
}
