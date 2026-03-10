using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SensitiveWords.Application.Interfaces;
using System.Data;
using System.Text.RegularExpressions;

namespace SensitiveWords.Infrastructure.Database
{
    public class DatabaseInitializer
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly IHostEnvironment _environment;

        public DatabaseInitializer(IDbConnectionFactory connectionFactory, IHostEnvironment environment)
        {
            _connectionFactory = connectionFactory;
            _environment = environment;
        }

        public async Task InitializeAsync()
        {
            // Run only for local development
            if (!_environment.IsDevelopment())
                return;            

            using var connection = _connectionFactory.CreateConnection();

            await EnsureDatabaseExists();

            // Create database first
            await RunScript(connection, "database/migrations/init.sql");

            // Now run scripts inside the new DB
            await RunScript(connection, "database/procedures/stored_procedures.sql");
            await RunScript(connection, "database/seeds/seed_sensitive_words.sql");
        }

        private static async Task RunScript(IDbConnection connection, string path)
        {
            var root = GetSolutionRoot();
            var fullPath = Path.Combine(root, path);

            var script = await File.ReadAllTextAsync(fullPath);

            var batches = Regex.Split(script, @"^\s*GO\s*$",
                RegexOptions.Multiline | RegexOptions.IgnoreCase);

            foreach (var batch in batches)
            {
                if (string.IsNullOrWhiteSpace(batch))
                    continue;

                await connection.ExecuteAsync(batch);
            }
        }
        private static string GetSolutionRoot()
        {
            var directory = Directory.GetCurrentDirectory();

            while (!Directory.Exists(Path.Combine(directory, "database")))
            {
                directory = Directory.GetParent(directory)!.FullName;
            }

            return directory;
        }

        private async Task EnsureDatabaseExists()
        {
            var connectionString = _connectionFactory.GetConnectionString();

            var builder = new SqlConnectionStringBuilder(connectionString)
            {
                InitialCatalog = "master"
            };

            using var connection = new SqlConnection(builder.ConnectionString);

            var databaseName = new SqlConnectionStringBuilder(connectionString).InitialCatalog;

            var sql = $@"
            IF DB_ID('{databaseName}') IS NULL
            BEGIN
                CREATE DATABASE [{databaseName}]
            END";

            await connection.ExecuteAsync(sql);
        }
    }
}
