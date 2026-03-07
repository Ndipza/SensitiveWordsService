using SensitiveWords.Application.DTOs.SensitiveWords;

namespace SensitiveWords.Application.Interfaces
{
    public interface ISensitiveWordService
    {
        Task<IEnumerable<SensitiveWordResponse>> GetAllAsync();

        Task AddAsync(CreateSensitiveWordRequest request);

        Task UpdateAsync(int id, UpdateSensitiveWordRequest request);

        Task DeleteAsync(int id);
    }
}
