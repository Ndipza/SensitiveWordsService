using SensitiveWords.Application.DTOs.Sanitization;
using Swashbuckle.AspNetCore.Filters;

namespace SensitiveWords.Api.Swagger.Examples
{
    /// <summary>
    /// Returns an example instance of a <see cref="SanitizeResponse"/> for use in API documentation or testing
    /// </summary>
    public class SanitizeResponseExample : IExamplesProvider<SanitizeResponse>
    {
        /// <summary>
        /// Returns an example response demonstrating the format of sanitized output.
        /// </summary>
        /// <returns>A <see cref="SanitizeResponse"/> containing a sample sanitized output string.</returns>
        public SanitizeResponse GetExamples()
        {
            return new SanitizeResponse
            {
                Output = "****** * **** USERS"
            };
        }
    }
}
