using Microsoft.Extensions.DependencyInjection;
using SensitiveWords.Application.Algorithms;
using SensitiveWords.Application.Interfaces;
using System.Threading;

namespace SensitiveWords.Application.Services
{
    public class SensitiveWordEngine
    {
        private readonly IServiceProvider _serviceProvider;
        private SensitiveWordTrie _trie = new();
        private readonly object _lock = new();

        public SensitiveWordEngine(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public SensitiveWordTrie Trie => _trie;

        public async Task ReloadAsync()
        {
            using var scope = _serviceProvider.CreateScope();

            var repository = scope.ServiceProvider
                .GetRequiredService<ISensitiveWordRepository>();

            var words = await repository.GetAllAsync();

            var trie = new SensitiveWordTrie();

            foreach (var word in words)
                trie.AddWord(word.Word);

            lock (_lock)
            {
                _trie = trie;
            }
        }

        public void AddWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return;

            lock (_lock)
            {
                _trie.AddWord(word.ToUpperInvariant());
            }
        }

        public void RemoveWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return;

            lock (_lock)
            {
                _trie.RemoveWord(word.ToUpperInvariant());
            }
        }
    }
}
