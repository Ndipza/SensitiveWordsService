using Swashbuckle.AspNetCore.Filters;

namespace SensitiveWords.Api.Swagger.Examples
{
    /// <summary>
    /// Provides an example instance of a <see cref="SanitizeRequest"/> for use in API documentation or testing
    /// scenarios.
    /// </summary>
    /// <remarks>This class is typically used by tools such as Swagger or Swashbuckle to generate example
    /// payloads for API endpoints that accept a <see cref="SanitizeRequest"/>. The example demonstrates a typical input
    /// value that might be sanitized by the API.</remarks>
    public class SanitizeRequestExample : IExamplesProvider<SanitizeRequest>
    {
        /// <summary>
        /// Creates and returns an example instance of the SanitizeRequest class for demonstration or documentation
        /// purposes.
        /// </summary>
        /// <returns>A SanitizeRequest object populated with sample data that illustrates typical usage.</returns>
        public SanitizeRequest GetExamples()
        {
            return new SanitizeRequest
            {
                Input = "SELECT * FROM USERS"
            };
        }
    }
}
