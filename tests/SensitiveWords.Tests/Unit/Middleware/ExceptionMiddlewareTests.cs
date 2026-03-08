using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using SensitiveWords.Api.Middleware;

namespace SensitiveWords.Tests.Unit.Middleware
{
    public class ExceptionMiddlewareTests
    {
        [Fact]
        public async Task Middleware_ShouldHandleException_AndReturn500()
        {
            var context = new DefaultHttpContext();

            var middleware = new ExceptionMiddleware(
                _ => throw new Exception("Test error"),
                Mock.Of<ILogger<ExceptionMiddleware>>());

            await middleware.Invoke(context);

            context.Response.StatusCode.Should().Be(500);
        }
    }
}
