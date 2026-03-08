using SensitiveWords.Application.DTOs.Sanitization;
using Swashbuckle.AspNetCore.Filters;

namespace SensitiveWords.Api.Swagger.Examples
{
    public class SanitizeResponseExample : IExamplesProvider<SanitizeResponse>
    {
        public SanitizeResponse GetExamples()
        {
            return new SanitizeResponse
            {
                Output = "****** * **** USERS"
            };
        }
    }
}
