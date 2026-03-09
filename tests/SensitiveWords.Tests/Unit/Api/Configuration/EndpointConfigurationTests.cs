using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SensitiveWords.Api.Configuration;
using Xunit;

namespace SensitiveWords.Tests.Unit.Api.Configuration
{
    public class EndpointConfigurationTests
    {
        [Fact]
        public void Should_Map_Api_Endpoints()
        {
            var builder = WebApplication.CreateBuilder();

            builder.Services.AddControllers();
            builder.Services.AddHealthChecks(); // required for MapHealthChecks

            var app = builder.Build();

            var result = app.MapApiEndpoints();

            result.Should().Be(app);
        }
    }
}