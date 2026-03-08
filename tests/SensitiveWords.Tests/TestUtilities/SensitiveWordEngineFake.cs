using SensitiveWords.Application.Algorithms.Trie;
using SensitiveWords.Application.Interfaces;

namespace SensitiveWords.Tests.TestUtilities
{
    public class SensitiveWordEngineFake : ISensitiveWordEngine
    {
        public SensitiveWordTrie Trie { get; }

        public bool IsInitialized { get; private set; }

        public SensitiveWordEngineFake(SensitiveWordTrie trie)
        {
            Trie = trie;
        }

        public Task ReloadAsync() => Task.CompletedTask;

        public void AddWord(string word) => Trie.AddWord(word);

        public void RemoveWord(string word) => Trie.RemoveWord(word);
    }
}
