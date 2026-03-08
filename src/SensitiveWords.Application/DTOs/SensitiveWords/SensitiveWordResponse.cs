namespace SensitiveWords.Application.DTOs.SensitiveWords
{
    public class SensitiveWordResponse
    {
        public int Id { get; set; }

        public string Word { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
