using System.ComponentModel.DataAnnotations;

namespace SensitiveWords.Application.DTOs.SensitiveWords
{
    public class CreateSensitiveWordRequest
    {
        /// <summary>
        /// The sensitive word to be added to the system.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Word { get; set; } = string.Empty;
    }
}
