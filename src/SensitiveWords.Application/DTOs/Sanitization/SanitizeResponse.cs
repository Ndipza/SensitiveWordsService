namespace SensitiveWords.Application.DTOs.Sanitization
{
    public class SanitizeResponse
    {
        /// <summary>
        /// Sanitized output text with sensitive words masked.
        /// </summary>
        public string Output { get; set; } = string.Empty;
    }
}
