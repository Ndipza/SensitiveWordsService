using FluentAssertions;
using Microsoft.Extensions.Configuration;
using SensitiveWords.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensitiveWords.Tests.Unit.Infrastructure
{
    public class DbConnectionFactoryTests
    {
        [Fact]
        public void Should_Create_SqlConnection()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                { "ConnectionStrings:DefaultConnection", "Server=.;Database=TestDb;" }
                })
                .Build();

            var factory = new DbConnectionFactory(configuration);

            var connection = factory.CreateConnection();

            connection.Should().NotBeNull();
        }
    }
}
