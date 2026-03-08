namespace SensitiveWords.Api.Configuration
{
    public static class EndpointConfiguration
    {
        public static WebApplication MapApiEndpoints(this WebApplication app)
        {
            app.UseHttpsRedirection();

            app.MapControllers();

            app.MapHealthChecks("/health/live");
            app.MapHealthChecks("/health/ready");

            return app;
        }
    }
}
