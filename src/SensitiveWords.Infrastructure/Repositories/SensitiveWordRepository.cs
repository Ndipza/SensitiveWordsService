using System.Data;
using Dapper;
using SensitiveWords.Domain.Entities;
using SensitiveWords.Application.Interfaces;
using SensitiveWords.Infrastructure.Database;

namespace SensitiveWords.Infrastructure.Repositories
{
    public class SensitiveWordRepository : ISensitiveWordRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public SensitiveWordRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<SensitiveWord>> GetAllAsync()
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<SensitiveWord>(
                "spSensitiveWords_GetAll",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<SensitiveWord?> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<SensitiveWord>(
                "spSensitiveWords_GetById",
                new { Id = id },
                commandType: CommandType.StoredProcedure);
        }

        public async Task AddAsync(SensitiveWord word)
        {
            using var connection = _connectionFactory.CreateConnection();

            var id = await connection.ExecuteScalarAsync<int>(
                "spSensitiveWords_Insert",
                new { word.Word },
                commandType: CommandType.StoredProcedure);

            word.Id = id;
        }

        public async Task UpdateAsync(SensitiveWord word)
        {
            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(
                "spSensitiveWords_Update",
                new
                {
                    word.Id,
                    word.Word
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(
                "spSensitiveWords_Delete",
                new { Id = id },
                commandType: CommandType.StoredProcedure);
        }

    }
}
