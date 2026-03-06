using SensitiveWords.Application.Algorithms;

namespace SensitiveWords.Application.Services
{
    public class SanitizationService
    {
        private readonly SensitiveWordEngine _engine;

        public SanitizationService(SensitiveWordEngine engine)
        {
            _engine = engine;
        }

        public string Sanitize(string input)
        {
            var matcher = new SensitiveWordMatcher(_engine.Trie);

            return matcher.Sanitize(input);
        }
    }
}
