using Microsoft.Extensions.DependencyInjection;
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

            return services;
        }
    }
}