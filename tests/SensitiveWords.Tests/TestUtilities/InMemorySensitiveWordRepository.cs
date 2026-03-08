using SensitiveWords.Application.Interfaces;
using SensitiveWords.Domain.Entities;

namespace SensitiveWords.Tests.TestUtilities
{
    public class InMemorySensitiveWordRepository : ISensitiveWordRepository
    {
        private readonly List<SensitiveWord> _words =
        [
            new SensitiveWord { Id = 1, Word = "SELECT" },
            new SensitiveWord { Id = 2, Word = "DROP" }
        ];

        public Task<IEnumerable<SensitiveWord>> GetAllAsync()
            => Task.FromResult(_words.AsEnumerable());

        public Task<SensitiveWord?> GetByIdAsync(int id)
            => Task.FromResult(_words.FirstOrDefault(x => x.Id == id));

        public Task AddAsync(SensitiveWord word)
        {
            word.Id = _words.Count + 1;
            _words.Add(word);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(SensitiveWord word)
        {
            var existing = _words.First(x => x.Id == word.Id);
            existing.Word = word.Word;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            _words.RemoveAll(x => x.Id == id);
            return Task.CompletedTask;
        }
    }
}