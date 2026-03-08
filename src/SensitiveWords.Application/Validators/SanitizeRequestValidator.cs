using FluentValidation;
using SensitiveWords.Application.DTOs.Sanitization;

namespace SensitiveWords.Application.Validators
{
    public class SanitizeRequestValidator : AbstractValidator<SanitizeRequest>
    {
        public SanitizeRequestValidator()
        {
            RuleFor(x => x.Input)
                .NotEmpty()
                .WithMessage("Input text is required.")

                .MaximumLength(5000)
                .WithMessage("Input text cannot exceed 5000 characters.");
        }
    }
}
