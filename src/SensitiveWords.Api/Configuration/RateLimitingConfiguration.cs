using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace SensitiveWords.Api.Configuration
{
    public static class RateLimitingConfiguration
    {
        public static IServiceCollection AddRateLimitingPolicies(
            this IServiceCollection services,
            IWebHostEnvironment env)
        {
            if (!env.IsEnvironment("Testing"))
            {
                services.AddRateLimiter(options =>
                {
                    options.AddFixedWindowLimiter("SanitizePolicy", opt =>
                    {
                        opt.PermitLimit = 100;
                        opt.Window = TimeSpan.FromSeconds(10);
                        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                        opt.QueueLimit = 10;
                    });
                });
            }

            return services;
        }

        public static IApplicationBuilder UseApiRateLimiting(
            this IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            if (!env.IsEnvironment("Testing"))
            {
                app.UseRateLimiter();
            }

            return app;
        }
    }
}
