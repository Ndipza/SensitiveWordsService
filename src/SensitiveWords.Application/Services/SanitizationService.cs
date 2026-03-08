using Microsoft.Extensions.Logging;
using SensitiveWords.Application.Interfaces;

public class SanitizationService : ISanitizationService
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

        _logger.LogDebug("Sanitizing input of length {Length}", input.Length);

        var matcher = new SensitiveWordMatcher(_engine.Trie);

        return matcher.Sanitize(input);
    }
}