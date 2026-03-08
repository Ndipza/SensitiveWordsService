using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using SensitiveWords.Application.Interfaces;
using SensitiveWords.Application.Services;
using SensitiveWords.Application.Services.Engine;
using SensitiveWords.Domain.Entities;

namespace SensitiveWords.Tests.Unit.Services
{
    public class SensitiveWordEngineTests
    {
        [Fact]
        public async Task ReloadAsync_ShouldLoadWordsIntoTrie()
        {
            var repository = new Mock<ISensitiveWordRepository>();

            repository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<SensitiveWord>
                {
            new() { Word = "SELECT" }
                });

            var services = new ServiceCollection();
            services.AddSingleton(repository.Object);

            var provider = services.BuildServiceProvider();

            var logger = Mock.Of<ILogger<SensitiveWordEngine>>();

            var engine = new SensitiveWordEngine(provider, logger);

            await engine.ReloadAsync();

            var matcher = new SensitiveWordMatcher(engine.Trie);

            var result = matcher.Sanitize("SELECT");

            result.Should().Be("******");
        }
    }
}
