using SensitiveWords.Domain.Entities;

namespace SensitiveWords.Application.Interfaces
{
    public interface ISensitiveWordRepository
    {
        Task<IEnumerable<SensitiveWord>> GetAllAsync();

        Task<int> CreateAsync(string word);

        Task UpdateAsync(int id, string word);

        Task DeleteAsync(int id);
    }
}
