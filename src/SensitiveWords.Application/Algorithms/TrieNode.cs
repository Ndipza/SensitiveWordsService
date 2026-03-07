using System.Collections.Concurrent;

namespace SensitiveWords.Application.Algorithms
{
    public sealed class TrieNode
    {
        private readonly Dictionary<char, TrieNode> _children = new();

        public IReadOnlyDictionary<char, TrieNode> Children => _children;

        public bool IsEndOfWord { get; set; }

        public string? Word { get; set; }

        public bool TryGetChild(char key, out TrieNode node)
            => _children.TryGetValue(key, out node);

        public void AddChild(char key, TrieNode node)
            => _children[key] = node;

        public bool RemoveChild(char key)
            => _children.Remove(key);
    }
}
