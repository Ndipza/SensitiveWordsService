namespace SensitiveWords.Application.Algorithms
{
    public sealed class SensitiveWordMatcher
    {
        private readonly SensitiveWordTrie _trie;

        public SensitiveWordMatcher(SensitiveWordTrie trie)
        {
            _trie = trie;
        }

        public string Sanitize(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            var buffer = input.ToCharArray();

            for (int i = 0; i < buffer.Length; i++)
            {
                var node = _trie.Root;
                int j = i;

                while (j < buffer.Length &&
                       node.TryGetChild(char.ToUpperInvariant(buffer[j]), out node))
                {
                    if (node.IsEndOfWord && node.Word != null)
                    {
                        Replace(buffer, i, j);
                    }

                    j++;
                }
            }

            return new string(buffer);
        }

        private static void Replace(char[] buffer, int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                buffer[i] = '*';
            }
        }
    }
}
