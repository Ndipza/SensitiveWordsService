using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SensitiveWords.Application.Interfaces;
using SensitiveWords.Domain.Entities;
using SensitiveWords.Infrastructure.Repositories;
using SensitiveWords.Tests.TestUtilities;
using System.Data;

namespace SensitiveWords.Tests.Unit.Infrastructure
{
    public class SensitiveWordRepositoryTests
    {
        [Fact]
        public async Task GetAllAsync_Should_Return_SensitiveWords()
        {
            var repo = new InMemorySensitiveWordRepository();

            await repo.AddAsync(new SensitiveWord { Word = "badword" });

            var result = await repo.GetAllAsync();

            result.Should().Contain(x => x.Word == "badword");
        }


        [Fact]
        public async Task GetAllAsync_Should_LogError_When_Exception_Occurs()
        {
            var factoryMock = new Mock<IDbConnectionFactory>();
            var loggerMock = new Mock<ILogger<SensitiveWordRepository>>();

            factoryMock
                .Setup(f => f.CreateConnection())
                .Throws(new Exception("DB failure"));

            var repository = new SensitiveWordRepository(
                factoryMock.Object,
                loggerMock.Object);

            Func<Task> act = async () => await repository.GetAllAsync();

            await act.Should().ThrowAsync<Exception>();

            loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }
    }
}
