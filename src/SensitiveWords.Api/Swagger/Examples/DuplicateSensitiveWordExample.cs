using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace SensitiveWords.Api.Swagger.Examples
{
    /// <summary>
    /// Returns an example of a problem details response for a duplicate sensitive word error, which can be used in API documentation or testing scenarios to illustrate the expected response when a client attempts to create a sensitive word that already exists in the system.
    /// </summary>
    public class DuplicateSensitiveWordExample : IExamplesProvider<ProblemDetails>
    {
        /// <summary>
        /// Returns an example instance of a <see cref="ProblemDetails"/> object that represents a conflict error due to a duplicate sensitive word. This example can be used in API documentation to show clients what the response would look like if they attempt to create a sensitive word that already exists.
        /// </summary>
        /// <returns></returns>
        public ProblemDetails GetExamples()
        {
            return new ProblemDetails
            {
                Title = "Duplicate sensitive word",
                Status = 409,
                Detail = "Sensitive word 'DROP' already exists."
            };
        }
    }
}
