using SensitiveWords.Domain.Entities;

namespace SensitiveWords.Application.Algorithms
{
    public sealed class SensitiveWordTrie
    {
        private readonly TrieNode _root = new();

        public TrieNode Root => _root;

        public void AddWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return;

            var currentNode = _root;

            foreach (var character in word.ToUpperInvariant())
            {
                currentNode = currentNode.GetOrAddChild(character);
            }

            currentNode.IsEndOfWord = true;
            currentNode.Word = word;
        }

        public void Build(IEnumerable<SensitiveWord> words)
        {
            foreach (var word in words)
            {
                AddWord(word.Word);
            }
        }
    }
}
