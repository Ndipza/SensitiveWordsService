namespace SensitiveWords.Application.Algorithms
{
    public class SensitiveWordTrie
    {
        private readonly TrieNode _root = new();

        public void AddWord(string word)
        {
            var node = _root;

            foreach (var c in word.ToUpper())
            {
                if (!node.Children.ContainsKey(c))
                    node.Children[c] = new TrieNode();

                node = node.Children[c];
            }

            node.IsEndOfWord = true;
        }

        public TrieNode Root => _root;
    }
}
