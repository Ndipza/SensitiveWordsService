using FluentAssertions;

namespace SensitiveWords.Tests.Unit.Algorithms.Trie
{
    public class SensitiveWordTrieTests
    {
        [Fact]
        public void Trie_ShouldRemoveWord()
        {
            var trie = new SensitiveWordTrie();

            trie.AddWord("DELETE");
            trie.RemoveWord("DELETE");

            var matcher = new SensitiveWordMatcher(trie);

            var result = matcher.Sanitize("DELETE FROM USERS");

            result.Should().Be("DELETE FROM USERS");
        }
    }
}
