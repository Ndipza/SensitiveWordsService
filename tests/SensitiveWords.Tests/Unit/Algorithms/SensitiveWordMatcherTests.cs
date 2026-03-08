using FluentAssertions;

namespace SensitiveWords.Tests.Unit.Algorithms
{
    public class SensitiveWordMatcherTests
    {
        [Fact]
        public void Matcher_ShouldDetectSensitiveWord()
        {
            var trie = new SensitiveWordTrie();
            trie.AddWord("DROP");

            var matcher = new SensitiveWordMatcher(trie);

            var result = matcher.Sanitize("DROP TABLE USERS");

            result.Should().Contain("****");
        }

        [Fact]
        public void Matcher_ShouldIgnoreNonSensitiveWords()
        {
            var trie = new SensitiveWordTrie();
            trie.AddWord("DELETE");

            var matcher = new SensitiveWordMatcher(trie);

            var result = matcher.Sanitize("HELLO WORLD");

            result.Should().Be("HELLO WORLD");
        }

        [Fact]
        public void Matcher_ShouldHandleCaseInsensitiveMatches()
        {
            var trie = new SensitiveWordTrie();
            trie.AddWord("DROP");

            var matcher = new SensitiveWordMatcher(trie);

            var result = matcher.Sanitize("drop table");

            result.Should().Contain("****");
        }

        [Fact]
        public void Matcher_ShouldPreferLongestMatch()
        {
            var trie = new SensitiveWordTrie();
            trie.AddWord("BAD");
            trie.AddWord("BADWORD");

            var matcher = new SensitiveWordMatcher(trie);

            var result = matcher.Sanitize("BADWORD");

            result.Should().Be("*******");
        }
    }
}