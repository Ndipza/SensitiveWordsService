using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SensitiveWords.Application.Interfaces;

namespace SensitiveWords.Tests.Unit.Services
{
    public class SanitizationServiceTests
    {
        
        [Fact]
        public void Sanitize_ShouldMaskSensitiveWords()
        {
            // Arrange
            var trie = new SensitiveWordTrie();
            trie.AddWord("SELECT");

            var engineMock = new Mock<ISensitiveWordEngine>();
            engineMock.Setup(e => e.Trie).Returns(trie);

            var logger = Mock.Of<ILogger<SanitizationService>>();

            var service = new SanitizationService(engineMock.Object, logger);

            // Act
            var result = service.Sanitize("SELECT * FROM USERS");

            // Assert
            result.Should().Contain("******");
        }

        [Fact]
        public void Sanitize_ShouldReturnSameText_WhenNoSensitiveWords()
        {
            // Arrange
            var trie = new SensitiveWordTrie();

            var engineMock = new Mock<ISensitiveWordEngine>();
            engineMock.Setup(e => e.Trie).Returns(trie);

            var logger = Mock.Of<ILogger<SanitizationService>>();

            var service = new SanitizationService(engineMock.Object, logger);

            // Act
            var result = service.Sanitize("HELLO WORLD");

            // Assert
            result.Should().Be("HELLO WORLD");
        }
    }
}
