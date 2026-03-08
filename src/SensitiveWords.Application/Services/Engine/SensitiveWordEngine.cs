using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SensitiveWords.Application.Algorithms;
using SensitiveWords.Application.Interfaces;

namespace SensitiveWords.Application.Services
{
    public class SensitiveWordEngine : ISensitiveWordEngine
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<SensitiveWordEngine> _logger;

        private SensitiveWordTrie _trie = new();
        private readonly object _lock = new();

        public SensitiveWordEngine(
            IServiceProvider serviceProvider,
            ILogger<SensitiveWordEngine> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public SensitiveWordTrie Trie => _trie;

        public bool IsInitialized { get; private set; }

        public async Task ReloadAsync()
        {
            _logger.LogInformation("Reloading sensitive words into Trie.");

            try
            {
                using var scope = _serviceProvider.CreateScope();

                var repository = scope.ServiceProvider
                    .GetRequiredService<ISensitiveWordRepository>();

                var words = await repository.GetAllAsync();

                var trie = new SensitiveWordTrie();

                int count = 0;

                foreach (var word in words)
                {
                    if (string.IsNullOrWhiteSpace(word.Word))
                        continue;

                    trie.AddWord(word.Word);
                    count++;
                }

                lock (_lock)
                {
                    _trie = trie;
                }

                _logger.LogInformation(
                    "Sensitive word Trie successfully reloaded with {Count} words.",
                    count);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Failed to reload sensitive words from repository.");

                throw;
            }
        }

        public void AddWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                _logger.LogWarning("Attempted to add an empty sensitive word.");
                return;
            }

            try
            {
                lock (_lock)
                {
                    _trie.AddWord(word.ToUpperInvariant());
                }

                _logger.LogInformation("Sensitive word added to Trie: {Word}", word);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error adding sensitive word {Word} to Trie.",
                    word);

                throw;
            }
        }

        public void RemoveWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                _logger.LogWarning("Attempted to remove an empty sensitive word.");
                return;
            }

            try
            {
                lock (_lock)
                {
                    _trie.RemoveWord(word.ToUpperInvariant());
                }

                _logger.LogInformation("Sensitive word removed from Trie: {Word}", word);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error removing sensitive word {Word} from Trie.",
                    word);

                throw;
            }
        }
    }
}