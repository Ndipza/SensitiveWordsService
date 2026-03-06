using System.Collections.Concurrent;

namespace SensitiveWords.Application.Algorithms
{
    public sealed class TrieNode
    {
        private readonly ConcurrentDictionary<char, TrieNode> _children = new();

        public bool IsEndOfWord { get; set; }

        public string? Word { get; set; }

        public IReadOnlyDictionary<char, TrieNode> Children => _children;

        public TrieNode GetOrAddChild(char character)
        {
            return _children.GetOrAdd(character, _ => new TrieNode());
        }

        public bool TryGetChild(char character, out TrieNode node)
        {
            return _children.TryGetValue(character, out node!);
        }
    }
}
