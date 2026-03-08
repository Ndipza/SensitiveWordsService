using SensitiveWords.Application.Algorithms.Trie;

namespace SensitiveWords.Application.Interfaces
{
    public interface ISensitiveWordEngine
    {
        SensitiveWordTrie Trie { get; }
        bool IsInitialized { get; }

        Task ReloadAsync();

        void AddWord(string word);

        void RemoveWord(string word);
    }
}
