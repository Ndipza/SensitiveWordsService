namespace SensitiveWords.Application.Algorithms
{
    public class SensitiveWordMatcher
    {
        private readonly SensitiveWordTrie _trie;

        public SensitiveWordMatcher(SensitiveWordTrie trie)
        {
            _trie = trie;
        }

        public string Sanitize(string input)
        {
            var chars = input.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                var node = _trie.Root;
                int j = i;

                while (j < chars.Length &&
                       node.Children.TryGetValue(char.ToUpper(chars[j]), out node))
                {
                    if (node.IsEndOfWord)
                    {
                        for (int k = i; k <= j; k++)
                            chars[k] = '*';
                    }

                    j++;
                }
            }

            return new string(chars);
        }
    }
}
