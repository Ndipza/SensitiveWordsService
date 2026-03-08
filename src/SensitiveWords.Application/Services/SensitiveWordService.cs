using Microsoft.Extensions.Logging;
using SensitiveWords.Application.DTOs.SensitiveWords;
using SensitiveWords.Application.Exceptions;
using SensitiveWords.Application.Interfaces;
using SensitiveWords.Domain.Entities;

namespace SensitiveWords.Application.Services
{
    public class SensitiveWordService : ISensitiveWordService
    {
        private readonly ISensitiveWordRepository _repository;
        private readonly ISensitiveWordEngine _engine;
        private readonly ILogger<SensitiveWordService> _logger;

        public SensitiveWordService(
            ISensitiveWordRepository repository,
            ISensitiveWordEngine engine,
            ILogger<SensitiveWordService> logger)
        {
            _repository = repository;
            _engine = engine;
            _logger = logger;
        }

        public async Task<IEnumerable<SensitiveWordResponse>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all sensitive words.");

            var words = await _repository.GetAllAsync();

            return words.Select(w => new SensitiveWordResponse
            {
                Id = w.Id,
                Word = w.Word,
                CreatedAt = w.CreatedAt
            });
        }

        public async Task AddAsync(CreateSensitiveWordRequest request)
        {
            var normalized = request.Word.Trim();

            if (string.IsNullOrWhiteSpace(normalized))
            {
                _logger.LogWarning("Invalid sensitive word submitted.");
                throw new ArgumentException("Sensitive word cannot be empty.");
            }

            var entity = new SensitiveWord
            {
                Word = normalized
            };

            _logger.LogInformation("Adding sensitive word: {Word}", entity.Word);

            await _repository.AddAsync(entity);

            _engine.AddWord(entity.Word);

            _logger.LogInformation("Sensitive word added successfully: {Word}", entity.Word);
        }

        public async Task UpdateAsync(int id, UpdateSensitiveWordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Word))
            {
                _logger.LogWarning("Attempted to update sensitive word {Id} with empty value.", id);
                throw new ArgumentException("Sensitive word cannot be empty.");
            }

            var existing = await _repository.GetByIdAsync(id);

            if (existing == null)
            {
                _logger.LogWarning("Sensitive word with id {Id} not found for update.", id);
                throw new NotFoundException($"Sensitive word with id {id} was not found.");
            }

            var oldWord = existing.Word;

            existing.Word = request.Word.Trim();

            _logger.LogInformation(
                "Updating sensitive word {Id}: {OldWord} -> {NewWord}",
                id,
                oldWord,
                existing.Word);

            await _repository.UpdateAsync(existing);

            // Update Trie
            _engine.RemoveWord(oldWord);
            _engine.AddWord(existing.Word);

            _logger.LogInformation("Sensitive word updated successfully: {Id}", id);
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);

            if (existing == null)
            {
                _logger.LogWarning("Sensitive word with id {Id} not found for deletion.", id);
                throw new NotFoundException($"Sensitive word with id {id} was not found.");
            }

            _logger.LogInformation("Deleting sensitive word {Id}: {Word}", id, existing.Word);

            await _repository.DeleteAsync(id);

            _engine.RemoveWord(existing.Word);

            _logger.LogInformation("Sensitive word deleted successfully: {Id}", id);
        }
    }
}