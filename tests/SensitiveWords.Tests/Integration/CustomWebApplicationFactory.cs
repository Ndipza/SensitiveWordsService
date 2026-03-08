using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using SensitiveWords.Application.Interfaces;
using SensitiveWords.Tests.TestUtilities;

namespace SensitiveWords.Tests.Integration
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                // Replace repository with in-memory implementation
                var descriptor = services
                    .SingleOrDefault(d => d.ServiceType == typeof(ISensitiveWordRepository));

                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddSingleton<ISensitiveWordRepository, InMemorySensitiveWordRepository>();

                // Fake engine
                var trie = new SensitiveWordTrie();
                trie.AddWord("SELECT");
                trie.AddWord("DROP");

                services.AddSingleton<ISensitiveWordEngine>(
                    new SensitiveWordEngineFake(trie));
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}