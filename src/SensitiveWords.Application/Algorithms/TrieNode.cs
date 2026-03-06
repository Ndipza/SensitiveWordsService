namespace SensitiveWords.Application.Algorithms
{
    public class TrieNode
    {
        public Dictionary<char, TrieNode> Children { get; } = new();

        public bool IsEndOfWord { get; set; }
    }
}
