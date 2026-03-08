using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace SensitiveWords.Api.Swagger.Examples
{
    /// <summary>
    /// Returns an example of a 404 Not Found response for API documentation purposes, illustrating the structure of a ProblemDetails object
    /// </summary>
    public class NotFoundExample : IExamplesProvider<ProblemDetails>
    {
        /// <summary>
        /// Returns an example instance of a ProblemDetails object representing a 404 Not Found error, which can be used in API documentation to show what a typical error response might look like when a requested resource is not found.
        /// </summary>
        /// <returns></returns>
        public ProblemDetails GetExamples()
        {
            return new ProblemDetails
            {
                Title = "Sensitive word not found",
                Status = 404,
                Detail = "Sensitive word with id 10 was not found."
            };
        }
    }
}
