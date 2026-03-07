using System.ComponentModel.DataAnnotations;

namespace SensitiveWords.Application.DTOs.SensitiveWords
{
    public class UpdateSensitiveWordRequest
    {
        /// <summary>
        /// Updated sensitive word value.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Word { get; set; } = string.Empty;
    }
}
