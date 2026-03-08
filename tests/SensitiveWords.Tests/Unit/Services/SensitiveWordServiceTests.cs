using FluentAssertions;
using Moq;
using SensitiveWords.Application.DTOs.SensitiveWords;
using SensitiveWords.Application.Exceptions;
using SensitiveWords.Application.Interfaces;
using SensitiveWords.Application.Services;
using SensitiveWords.Domain.Entities;

namespace SensitiveWords.Tests.Unit.Services
{
    public class SensitiveWordServiceTests
    {
        private readonly Mock<ISensitiveWordRepository> _repository = new();
        private readonly Mock<ISensitiveWordEngine> _engine = new();

        private SensitiveWordService CreateService()
        {
            return new SensitiveWordService(
                _repository.Object,
                _engine.Object,
                Mock.Of<Microsoft.Extensions.Logging.ILogger<SensitiveWordService>>());
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnMappedWords()
        {
            var words = new List<SensitiveWord>
        {
            new() { Id = 1, Word = "SELECT" }
        };

            _repository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(words);

            var service = CreateService();

            var result = await service.GetAllAsync();

            result.Should().HaveCount(1);
            result.First().Word.Should().Be("SELECT");
        }

        [Fact]
        public async Task AddAsync_ShouldInsertWordAndUpdateTrie()
        {
            var request = new CreateSensitiveWordRequest
            {
                Word = "DROP"
            };

            var service = CreateService();

            await service.AddAsync(request);

            _repository.Verify(r => r.AddAsync(It.IsAny<SensitiveWord>()), Times.Once);
            _engine.Verify(e => e.AddWord("DROP"), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveWordFromTrie()
        {
            var entity = new SensitiveWord
            {
                Id = 1,
                Word = "DELETE"
            };

            _repository.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(entity);

            var service = CreateService();

            await service.DeleteAsync(1);

            _repository.Verify(r => r.DeleteAsync(1), Times.Once);
            _engine.Verify(e => e.RemoveWord("DELETE"), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateTrieAndRepository()
        {
            var entity = new SensitiveWord
            {
                Id = 1,
                Word = "OLDWORD"
            };

            _repository.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(entity);

            var service = CreateService();

            await service.UpdateAsync(1, new UpdateSensitiveWordRequest
            {
                Word = "NEWWORD"
            });

            _repository.Verify(r => r.UpdateAsync(It.IsAny<SensitiveWord>()), Times.Once);
            _engine.Verify(e => e.RemoveWord("OLDWORD"), Times.Once);
            _engine.Verify(e => e.AddWord("NEWWORD"), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ShouldThrow_WhenWordIsEmpty()
        {
            var request = new CreateSensitiveWordRequest
            {
                Word = ""
            };

            var service = CreateService();

            Func<Task> act = async () => await service.AddAsync(request);

            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task AddAsync_ShouldCallRepositoryAndEngine()
        {
            var request = new CreateSensitiveWordRequest
            {
                Word = "SELECT"
            };

            var service = CreateService();

            await service.AddAsync(request);

            _repository.Verify(r => r.AddAsync(It.IsAny<SensitiveWord>()), Times.Once);

            _engine.Verify(e => e.AddWord("SELECT"), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateWordAndTrie()
        {
            var existing = new SensitiveWord
            {
                Id = 1,
                Word = "SELECT"
            };

            _repository.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(existing);

            var request = new UpdateSensitiveWordRequest
            {
                Word = "DROP"
            };

            var service = CreateService();

            await service.UpdateAsync(1, request);

            _repository.Verify(r => r.UpdateAsync(It.IsAny<SensitiveWord>()), Times.Once);

            _engine.Verify(e => e.RemoveWord("SELECT"), Times.Once);

            _engine.Verify(e => e.AddWord("DROP"), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrow_WhenWordNotFound()
        {
            _repository.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync((SensitiveWord?)null);

            var request = new UpdateSensitiveWordRequest
            {
                Word = "DROP"
            };

            var service = CreateService();

            Func<Task> act = async () => await service.UpdateAsync(1, request);

            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrow_WhenWordNotFound()
        {
            _repository.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync((SensitiveWord?)null);

            var service = CreateService();

            Func<Task> act = async () => await service.DeleteAsync(1);

            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteWordAndUpdateTrie()
        {
            var existing = new SensitiveWord
            {
                Id = 1,
                Word = "DROP"
            };

            _repository.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(existing);

            var service = CreateService();

            await service.DeleteAsync(1);

            _repository.Verify(r => r.DeleteAsync(1), Times.Once);

            _engine.Verify(e => e.RemoveWord("DROP"), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmpty_WhenNoWords()
        {
            _repository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<SensitiveWord>());

            var service = CreateService();

            var result = await service.GetAllAsync();

            result.Should().BeEmpty();
        }
    }
}
