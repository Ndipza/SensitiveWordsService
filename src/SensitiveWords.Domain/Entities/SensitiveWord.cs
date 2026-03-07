namespace SensitiveWords.Domain.Entities
{
    public class SensitiveWord
    {
        public int Id { get; set; }

        public string Word { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
