using FluentAssertions;
using SensitiveWords.Application.Validators;

namespace SensitiveWords.Tests.Unit.Validators
{
    public class SanitizeRequestValidatorTests
    {
        private readonly SanitizeRequestValidator _validator = new();

        [Fact]
        public void ShouldFail_WhenInputIsEmpty()
        {
            var request = new SanitizeRequest
            {
                Input = ""
            };

            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void ShouldPass_WhenInputIsValid()
        {
            var request = new SanitizeRequest
            {
                Input = "SELECT * FROM USERS"
            };

            var result = _validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }
    }
}
