using SensitiveWords.Domain.Entities;

namespace SensitiveWords.Application.Interfaces
{
    public interface ISensitiveWordRepository
    {
        Task<IEnumerable<SensitiveWord>> GetAllAsync();

        Task<SensitiveWord?> GetByIdAsync(int id);

        Task AddAsync(SensitiveWord word);

        Task UpdateAsync(SensitiveWord word);

        Task DeleteAsync(int id);
    }
}
