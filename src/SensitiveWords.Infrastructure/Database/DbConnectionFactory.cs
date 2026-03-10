using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SensitiveWords.Application.Interfaces;
using System.Data;

namespace SensitiveWords.Infrastructure.Database
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
