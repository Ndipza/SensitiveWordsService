using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using SensitiveWords.Application.Exceptions;
using SensitiveWords.Application.Interfaces;
using SensitiveWords.Domain.Entities;
using SensitiveWords.Infrastructure.Database;
using System.Data;

namespace SensitiveWords.Infrastructure.Repositories
{
    public class SensitiveWordRepository : ISensitiveWordRepository
    {
        private readonly DbConnectionFactory _connectionFactory;
        private readonly ILogger<SensitiveWordRepository> _logger;

        public SensitiveWordRepository(
            DbConnectionFactory connectionFactory,
            ILogger<SensitiveWordRepository> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public async Task<IEnumerable<SensitiveWord>> GetAllAsync()
        {
            try
            {
                using var connection = _connectionFactory.CreateConnection();

                return await connection.QueryAsync<SensitiveWord>(
                    "spSensitiveWords_GetAll",
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sensitive words.");
                throw;
            }
        }

        public async Task<SensitiveWord?> GetByIdAsync(int id)
        {
            try
            {
                using var connection = _connectionFactory.CreateConnection();

                return await connection.QueryFirstOrDefaultAsync<SensitiveWord>(
                    "spSensitiveWords_GetById",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sensitive word with id {Id}", id);
                throw;
            }
        }

        public async Task AddAsync(SensitiveWord word)
        {
            try
            {
                using var connection = _connectionFactory.CreateConnection();

                var id = await connection.ExecuteScalarAsync<int>(
                    "spSensitiveWords_Insert",
                    new { Word = word.Word },
                    commandType: CommandType.StoredProcedure);

                word.Id = id;

                _logger.LogInformation("Sensitive word '{Word}' inserted with id {Id}", word.Word, id);
            }
            catch (SqlException ex) when (ex.Number == 50001)
            {
                _logger.LogWarning("Duplicate sensitive word attempt: {Word}", word.Word);
                throw new DuplicateSensitiveWordException(word.Word);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting sensitive word '{Word}'", word.Word);
                throw;
            }
        }

        public async Task UpdateAsync(SensitiveWord word)
        {
            try
            {
                using var connection = _connectionFactory.CreateConnection();

                await connection.ExecuteAsync(
                    "spSensitiveWords_Update",
                    new
                    {
                        Id = word.Id,
                        Word = word.Word
                    },
                    commandType: CommandType.StoredProcedure);

                _logger.LogInformation("Sensitive word {Id} updated to '{Word}'", word.Id, word.Word);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating sensitive word {Id}", word.Id);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                using var connection = _connectionFactory.CreateConnection();

                await connection.ExecuteAsync(
                    "spSensitiveWords_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);

                _logger.LogInformation("Sensitive word {Id} deleted", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting sensitive word {Id}", id);
                throw;
            }
        }

    }
}
