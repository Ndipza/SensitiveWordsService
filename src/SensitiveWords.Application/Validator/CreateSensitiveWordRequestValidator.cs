using FluentValidation;
using SensitiveWords.Application.DTOs.SensitiveWords;

namespace SensitiveWords.Application.Validator
{
    public class CreateSensitiveWordRequestValidator
    : AbstractValidator<CreateSensitiveWordRequest>
    {
        public CreateSensitiveWordRequestValidator()
        {
            RuleFor(x => x.Word)
                .NotEmpty().WithMessage("Word is required.")
                .MinimumLength(2)
                .MaximumLength(100);
        }
    }
}
