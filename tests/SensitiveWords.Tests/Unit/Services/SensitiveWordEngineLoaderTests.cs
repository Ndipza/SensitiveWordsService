using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Polly;
using Polly.Registry;
using SensitiveWords.Application.Common.Policies;
using SensitiveWords.Application.Interfaces;
using SensitiveWords.Application.Services;
using SensitiveWords.Application.Services.Engine;
using SensitiveWords.Domain.Entities;

namespace SensitiveWords.Tests.Unit.Services
{
    public class SensitiveWordEngineLoaderTests
    {
        [Fact]
        public async Task StartAsync_ShouldReloadEngine()
        {
            // Arrange
            var engine = new Mock<ISensitiveWordEngine>();

            var logger = Mock.Of<ILogger<SensitiveWordEngineLoader>>();

            var registry = new PolicyRegistry();
            registry.Add(PollyPolicies.DatabaseRetry, Policy.NoOpAsync());

            var loader = new SensitiveWordEngineLoader(engine.Object, registry, logger);

            // Act
            await loader.StartAsync(CancellationToken.None);

            // Assert
            engine.Verify(e => e.ReloadAsync(), Times.Once);
        }

        [Fact]
        public async Task ReloadAsync_ShouldPopulateTrieWithWords()
        {
            var repository = new Mock<ISensitiveWordRepository>();

            repository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<SensitiveWord>
                {
                new() { Id = 1, Word = "SELECT" },
                new() { Id = 2, Word = "DROP" }
                });

            var services = new ServiceCollection();
            services.AddSingleton(repository.Object);

            var provider = services.BuildServiceProvider();

            var logger = Mock.Of<ILogger<SensitiveWordEngine>>();

            var engine = new SensitiveWordEngine(provider, logger);

            await engine.ReloadAsync();

            var matcher = new SensitiveWordMatcher(engine.Trie);

            var result = matcher.Sanitize("SELECT * FROM USERS");

            result.Should().Contain("******");
        }
    }
}
