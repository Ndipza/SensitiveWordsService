using SensitiveWords.Application.Algorithms;
using SensitiveWords.Application.Interfaces;

namespace SensitiveWords.Application.Services
{
    public class SensitiveWordEngine
    {
        private readonly ISensitiveWordRepository _repository;

        private SensitiveWordTrie _trie = new();

        public SensitiveWordEngine(ISensitiveWordRepository repository)
        {
            _repository = repository;
        }

        public SensitiveWordTrie Trie => _trie;

        public async Task ReloadAsync()
        {
            var words = await _repository.GetAllAsync();

            var trie = new SensitiveWordTrie();

            foreach (var word in words)
                trie.AddWord(word.Word);

            _trie = trie;
        }
    }
}
