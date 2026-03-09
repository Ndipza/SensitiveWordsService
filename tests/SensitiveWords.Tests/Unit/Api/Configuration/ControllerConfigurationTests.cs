using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using SensitiveWords.Api.Configuration;

namespace SensitiveWords.Tests.Unit.Api.Configuration
{
    public class ControllerConfigurationTests
    {
        [Fact]
        public void Should_Register_Controllers()
        {
            var services = new ServiceCollection();

            services.AddApiControllers();

            services.Should().NotBeNull();
        }
    }
}
