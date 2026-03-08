using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace SensitiveWords.Api.Swagger.Examples
{
    /// <summary>
    /// Returns an example of a 500 Internal Server Error response for API documentation purposes. This class implements the
    /// </summary>
    public class InternalServerErrorExample : IExamplesProvider<ProblemDetails>
    {
        /// <summary>
        /// Returns an example instance of the <see cref="ProblemDetails"/> class representing a generic internal server
        /// error.
        /// </summary>
        /// <returns>A <see cref="ProblemDetails"/> object pre-populated with example values for a 500 Internal Server Error.</returns>
        public ProblemDetails GetExamples()
        {
            return new ProblemDetails
            {
                Title = "Internal server error",
                Status = 500,
                Detail = "An unexpected error occurred while processing the request."
            };
        }
    }
}
