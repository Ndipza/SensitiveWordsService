using SensitiveWords.Application.DTOs.SensitiveWords;
using SensitiveWords.Application.Exceptions;
using SensitiveWords.Application.Interfaces;
using SensitiveWords.Domain.Entities;

namespace SensitiveWords.Application.Services
{
    public class SensitiveWordService : ISensitiveWordService
    {
        private readonly ISensitiveWordRepository _repository;
        private readonly SensitiveWordEngine _engine;

        public SensitiveWordService(
            ISensitiveWordRepository repository,
            SensitiveWordEngine engine)
        {
            _repository = repository;
            _engine = engine;
        }

        public async Task<IEnumerable<SensitiveWordResponse>> GetAllAsync()
        {
            var words = await _repository.GetAllAsync();

            return words.Select(w => new SensitiveWordResponse
            {
                Id = w.Id,
                Word = w.Word
            });
        }

        public async Task AddAsync(CreateSensitiveWordRequest request)
        {
            var entity = new SensitiveWord
            {
                Word = request.Word
            };

            await _repository.AddAsync(entity);

            _engine.AddWord(entity.Word);
        }

        public async Task UpdateAsync(int id, UpdateSensitiveWordRequest request)
        {
            var existing = await _repository.GetByIdAsync(id);

            if (existing == null)
                throw new NotFoundException($"Sensitive word with id {id} was not found.");

            existing.Word = request.Word;

            await _repository.UpdateAsync(existing);

            _engine.AddWord(existing.Word);
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);

            if (existing == null)
                throw new NotFoundException($"Sensitive word with id {id} was not found.");

            await _repository.DeleteAsync(id);

            _engine.RemoveWord(existing.Word);
        }
    }
}
