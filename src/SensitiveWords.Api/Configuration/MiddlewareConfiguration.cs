using SensitiveWords.Api.Middleware;

namespace SensitiveWords.Api.Configuration
{
    public static class MiddlewareConfiguration
    {
        public static IApplicationBuilder UseApiMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<CorrelationIdMiddleware>();
            app.UseMiddleware<RequestLoggingMiddleware>();

            return app;
        }

        public static IApplicationBuilder UseApiSecurity(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsEnvironment("Testing"))
            {
                app.UseAuthentication();
                app.UseAuthorization();
            }

            return app;
        }
    }
}
