using System.Net;
using Microsoft.AspNetCore.Mvc;
using SensitiveWords.Application.Exceptions;
using FluentValidation;

namespace SensitiveWords.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                _logger.LogInformation(
                    "Incoming request {Method} {Path}",
                    context.Request.Method,
                    context.Request.Path);

                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
            finally
            {
                stopwatch.Stop();

                _logger.LogInformation(
                    "Request completed {StatusCode} in {Elapsed}ms",
                    context.Response.StatusCode,
                    stopwatch.ElapsedMilliseconds);
            }
        }

        private async Task HandleException(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "Unhandled exception occurred");

            if (context.Response.HasStarted)
            {
                _logger.LogWarning("Response already started, cannot write error response.");
                return;
            }

            var (status, title) = exception switch
            {
                DuplicateSensitiveWordException => (HttpStatusCode.Conflict, "Duplicate sensitive word"),
                NotFoundException => (HttpStatusCode.NotFound, "Resource not found"),
                ValidationException => (HttpStatusCode.BadRequest, "Validation error"),
                _ => (HttpStatusCode.InternalServerError, "Unexpected server error")
            };

            var problem = new ProblemDetails
            {
                Status = (int)status,
                Title = title,
                Detail = exception.Message,
                Instance = context.Request.Path
            };

            problem.Extensions["traceId"] = context.TraceIdentifier;

            context.Response.Clear();
            context.Response.StatusCode = (int)status;

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}