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

            word = word.Trim().ToUpperInvariant();

            var current = _root;

            foreach (var c in word)
            {
                if (!current.TryGetChild(c, out var next))
                {
                    next = new TrieNode();
                    current.AddChild(c, next);
                }

                current = next;
            }

            current.IsEndOfWord = true;
            current.Word = word;
        }

        public void Build(IEnumerable<SensitiveWord> words)
        {
            foreach (var word in words)
            {
                AddWord(word.Word);
            }
        }

        public void RemoveWord(string word)
        {
            RemoveWord(_root, word, 0);
        }

        private bool RemoveWord(TrieNode node, string word, int depth)
        {
            if (node == null)
                return false;

            if (depth == word.Length)
            {
                if (!node.IsEndOfWord)
                    return false;

                node.IsEndOfWord = false;

                return node.Children.Count == 0;
            }

            char c = word[depth];

            if (!node.TryGetChild(c, out var child))
                return false;

            bool shouldDelete = RemoveWord(child, word, depth + 1);

            if (shouldDelete)
            {
                node.RemoveChild(c);

                return node.Children.Count == 0 && !node.IsEndOfWord;
            }

            return false;
        }
    }
}
