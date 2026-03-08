namespace SensitiveWords.Api.Middleware
{
    public class CorrelationIdMiddleware
    {
        private const string CorrelationHeader = "X-Correlation-ID";

        private readonly RequestDelegate _next;
        private readonly ILogger<CorrelationIdMiddleware> _logger;

        public CorrelationIdMiddleware(
            RequestDelegate next,
            ILogger<CorrelationIdMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var correlationId = context.Request.Headers[CorrelationHeader].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(correlationId))
                correlationId = Guid.NewGuid().ToString();

            context.Items[CorrelationHeader] = correlationId;
            context.Response.Headers[CorrelationHeader] = correlationId;

            using (_logger.BeginScope("CorrelationId:{CorrelationId}", correlationId))
            {
                await _next(context);
            }
        }
    }
}