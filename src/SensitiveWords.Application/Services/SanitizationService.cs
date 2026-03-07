using Microsoft.Extensions.Logging;
using SensitiveWords.Application.Algorithms;
using SensitiveWords.Application.Interfaces;

namespace SensitiveWords.Application.Services
{
    public class SanitizationService
    {
        private readonly ISensitiveWordEngine _engine;
        private readonly ILogger<SanitizationService> _logger;

        public SanitizationService(
            ISensitiveWordEngine engine,
            ILogger<SanitizationService> logger)
        {
            _engine = engine;
            _logger = logger;
        }

        public string Sanitize(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                _logger.LogWarning("Sanitize called with empty input.");
                return input ?? string.Empty;
            }

            try
            {
                _logger.LogDebug(
                    "Sanitizing text with length {Length}",
                    input.Length);

                var matcher = new SensitiveWordMatcher(_engine.Trie);

                var result = matcher.Sanitize(input);

                _logger.LogDebug("Sanitization completed.");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while sanitizing input.");

                throw;
            }
        }
    }
}
