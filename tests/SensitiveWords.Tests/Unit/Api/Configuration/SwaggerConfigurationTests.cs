using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using SensitiveWords.Api.Configuration;

namespace SensitiveWords.Tests.Unit.Api.Configuration
{
    public class SwaggerConfigurationTests
    {
        [Fact]
        public void Should_Register_Swagger_Services()
        {
            var services = new ServiceCollection();

            services.AddApiDocumentation();

            services.Should().NotBeNull();
        }
    }
}
