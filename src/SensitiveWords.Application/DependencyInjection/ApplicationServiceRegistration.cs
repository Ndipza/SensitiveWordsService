using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly.Registry;
using SensitiveWords.Application.Common.Policies;
using SensitiveWords.Application.Interfaces;
using SensitiveWords.Application.Services;
using SensitiveWords.Application.Services.Engine;

namespace SensitiveWords.Application.DependencyInjection
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            // Core application services
            services.AddScoped<ISanitizationService, SanitizationService>();
            services.AddScoped<ISensitiveWordService, SensitiveWordService>();

            // Trie engine (shared instance)
            services.AddSingleton<ISensitiveWordEngine, SensitiveWordEngine>();

            // Engine loader (startup background worker)
            services.AddHostedService<SensitiveWordEngineLoader>();

            // Polly policies
            services.AddSingleton<PolicyRegistry>(provider =>
            {
                var logger = provider.GetRequiredService<ILoggerFactory>()
                                     .CreateLogger("PollyPolicies");

                return PollyPolicies.CreateRegistry(logger);
            });

            return services;
        }
    }
}