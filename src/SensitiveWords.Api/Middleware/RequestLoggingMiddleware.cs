using SensitiveWords.Api.Extensions;

namespace SensitiveWords.Api.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(
            RequestDelegate next,
            ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            var correlationId = context.GetCorrelationId();

            _logger.LogInformation(
                "Incoming request {Method} {Path} CorrelationId:{CorrelationId}",
                context.Request.Method,
                context.Request.Path,
                correlationId);

            await _next(context);

            stopwatch.Stop();

            _logger.LogInformation(
                "Request completed {StatusCode} in {Elapsed}ms CorrelationId:{CorrelationId}",
                context.Response.StatusCode,
                stopwatch.ElapsedMilliseconds,
                correlationId);
        }
    }
}