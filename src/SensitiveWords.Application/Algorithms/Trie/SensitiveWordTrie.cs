using SensitiveWords.Application.Algorithms.Trie;
using SensitiveWords.Domain.Entities;

public sealed class SensitiveWordTrie
{
    private readonly TrieNode _root = new();

    /// <summary>
    /// Root node of the Trie.
    /// </summary>
    public TrieNode Root => _root;

    /// <summary>
    /// Adds a new word into the Trie structure.
    /// </summary>
    public void AddWord(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
            return;

        word = Normalize(word);

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

    /// <summary>
    /// Bulk loads words into the Trie.
    /// </summary>
    public void Build(IEnumerable<SensitiveWord> words)
    {
        foreach (var word in words)
        {
            AddWord(word.Word);
        }
    }

    public void RemoveWord(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
            return;

        RemoveWord(_root, Normalize(word), 0);
    }

    private static string Normalize(string word)
        => word.Trim().ToUpperInvariant();

    private bool RemoveWord(TrieNode node, string word, int depth)
    {
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