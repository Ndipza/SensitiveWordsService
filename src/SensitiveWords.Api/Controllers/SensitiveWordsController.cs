using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SensitiveWords.Api.Swagger.Examples;
using SensitiveWords.Application.DTOs.SensitiveWords;
using SensitiveWords.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace SensitiveWords.Api.Controllers
{
    /// <summary>
    /// Provides endpoints to manage sensitive words used by the sanitization engine.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/sensitive-words")]
    [Tags("Sensitive Words")]
    public sealed class SensitiveWordsController : ControllerBase
    {
        private readonly ISensitiveWordService _service;
        private readonly ILogger<SensitiveWordsController> _logger;

        public SensitiveWordsController(
            ISensitiveWordService service,
            ILogger<SensitiveWordsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all sensitive words.
        /// </summary>
        /// <returns>A collection of sensitive words.</returns>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Retrieve all sensitive words",
            Description = "Returns the full list of sensitive words stored in the system."
        )]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SanitizeResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        [ProducesResponseType(typeof(IEnumerable<SensitiveWordResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<SensitiveWordResponse>>> GetAll()
        {
            _logger.LogInformation("Retrieving all sensitive words");

            var result = await _service.GetAllAsync();

            return Ok(result);
        }

        /// <summary>
        /// Adds a new sensitive word.
        /// </summary>
        /// <param name="request">The sensitive word payload.</param>
        /// <returns>Returns HTTP 201 if the word was created successfully.</returns>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Create a new sensitive word",
            Description = "Adds a sensitive word that will be detected and masked by the sanitization engine."
        )]
        [SwaggerRequestExample(typeof(CreateSensitiveWordRequest), typeof(CreateSensitiveWordExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestExample))]        
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(DuplicateSensitiveWordExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(
            [FromBody, SwaggerParameter("Sensitive word payload", Required = true)]
            CreateSensitiveWordRequest request)
        {
            _logger.LogInformation("Creating sensitive word {Word}", request.Word);

            await _service.AddAsync(request);

            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Updates an existing sensitive word.
        /// </summary>
        /// <param name="id">Unique identifier of the sensitive word.</param>
        /// <param name="request">Updated word payload.</param>
        [HttpPut("{id:int}")]
        [SwaggerOperation(
            Summary = "Update a sensitive word",
            Description = "Updates an existing sensitive word used by the sanitization engine."
        )]
        [SwaggerRequestExample(typeof(CreateSensitiveWordRequest), typeof(CreateSensitiveWordExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(DuplicateSensitiveWordExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update(
            [SwaggerParameter("Sensitive word ID", Required = true)] int id,
            [FromBody] UpdateSensitiveWordRequest request)
        {
            _logger.LogInformation("Updating sensitive word with id {Id}", id);

            await _service.UpdateAsync(id, request);

            return NoContent();
        }

        /// <summary>
        /// Deletes a sensitive word.
        /// </summary>
        /// <param name="id">Unique identifier of the sensitive word.</param>
        [HttpDelete("{id:int}")]
        [SwaggerOperation(
            Summary = "Delete a sensitive word",
            Description = "Removes a sensitive word from the system."
        )]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(
            [SwaggerParameter("Sensitive word ID", Required = true)] int id)
        {
            _logger.LogInformation("Deleting sensitive word with id {Id}", id);

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}