using FluentAssertions;
using SensitiveWords.Application.Algorithms;

namespace SensitiveWords.Tests.Algorithms
{
    public class SensitiveWordMatcherTests
    {
        [Fact]
        public void Should_Mask_Sensitive_Word()
        {
            var trie = new SensitiveWordTrie();
            trie.AddWord("SELECT");

            var matcher = new SensitiveWordMatcher(trie);

            var result = matcher.Sanitize("SELECT * FROM users");

            result.Should().Be("****** * FROM users");
        }
    }
}
