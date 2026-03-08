using FluentAssertions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Moq;
using SensitiveWords.Application.HealthChecks;
using SensitiveWords.Application.Interfaces;

namespace SensitiveWords.Tests.Unit.HealthChecks
{
    public class TrieHealthCheckTests
    {
        [Fact]
        public async Task CheckHealthAsync_ShouldReturnHealthy_WhenTrieExists()
        {
            var engine = new Mock<ISensitiveWordEngine>();

            engine.Setup(e => e.Trie)
                  .Returns(new SensitiveWordTrie());

            engine.Setup(e => e.IsInitialized)
                  .Returns(true);

            var logger = Mock.Of<ILogger<TrieHealthCheck>>();

            var healthCheck = new TrieHealthCheck(engine.Object, logger);

            var result = await healthCheck.CheckHealthAsync(new HealthCheckContext());

            result.Status.Should().Be(HealthStatus.Healthy);
        }

        [Fact]
        public async Task CheckHealthAsync_ShouldReturnDegraded_WhenEngineNotInitialized()
        {
            var engine = new Mock<ISensitiveWordEngine>();

            engine.Setup(e => e.IsInitialized).Returns(false);

            var logger = Mock.Of<ILogger<TrieHealthCheck>>();

            var healthCheck = new TrieHealthCheck(engine.Object, logger);

            var result = await healthCheck.CheckHealthAsync(new HealthCheckContext());

            result.Status.Should().Be(HealthStatus.Degraded);
        }
    }
}
