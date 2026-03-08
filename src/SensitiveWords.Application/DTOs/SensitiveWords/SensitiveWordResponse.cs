namespace SensitiveWords.Application.DTOs.SensitiveWords
{
    /// <summary>
    /// Represents the response data for a sensitive word entry, including its identifier, text, and creation timestamp.
    /// </summary>
    public class SensitiveWordResponse
    {
        public int Id { get; set; }

        public string Word { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
