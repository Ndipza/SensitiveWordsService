using SensitiveWords.Application.DTOs.SensitiveWords;
using Swashbuckle.AspNetCore.Filters;

namespace SensitiveWords.Api.Swagger.Examples
{
    /// <summary>
    /// Provides an example instance of a request to create a sensitive word, for use in API documentation or testing
    /// scenarios.
    /// </summary>
    /// <remarks>This class is typically used by tools that generate example payloads for OpenAPI/Swagger
    /// documentation. It supplies a sample request demonstrating how to specify a sensitive word when creating a new
    /// entry.</remarks>
    public class CreateSensitiveWordExample : IExamplesProvider<CreateSensitiveWordRequest>
    {
        /// <summary>
        /// Creates and returns an example instance of a CreateSensitiveWordRequest for demonstration or testing
        /// purposes.
        /// </summary>
        /// <returns>A CreateSensitiveWordRequest object populated with sample data.</returns>
        public CreateSensitiveWordRequest GetExamples()
        {
            return new CreateSensitiveWordRequest
            {
                Word = "DROP"
            };
        }
    }
}
