using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SensitiveWords.Api.Swagger.Examples;
using SensitiveWords.Application.DTOs.Sanitization;
using SensitiveWords.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace SensitiveWords.Api.Controllers
{
    /// <summary>
    /// Provides endpoints for sanitizing text by masking sensitive words.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/sanitizer")]
    [Tags("Sanitizer")]
    [EnableRateLimiting("SanitizePolicy")]
    public sealed class SanitizerController : ControllerBase
    {
        private readonly ISanitizationService _service;
        private readonly ILogger<SanitizerController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SanitizerController"/>.
        /// </summary>
        /// <param name="service">Service responsible for sanitizing text.</param>
        /// <param name="logger">Logger used for diagnostics and monitoring.</param>
        public SanitizerController(
            ISanitizationService service,
            ILogger<SanitizerController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Sanitizes input text by masking sensitive words.
        /// </summary>
        /// <param name="request">Request containing the input text to sanitize.</param>
        /// <returns>The sanitized output with sensitive words masked.</returns>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Sanitize input text",
            Description = "Receives a text string and replaces sensitive words with masked characters."
        )]
        [SwaggerRequestExample(typeof(SanitizeRequest), typeof(SanitizeRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SanitizeResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
        [ProducesResponseType(typeof(SanitizeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public ActionResult<SanitizeResponse> Sanitize(
            [FromBody, SwaggerParameter("Input text payload to sanitize", Required = true)]
        SanitizeRequest request)
        {
            _logger.LogInformation("Sanitization request received. Input length: {Length}", request.Input?.Length ?? 0);

            if (string.IsNullOrWhiteSpace(request.Input))
            {
                _logger.LogWarning("Sanitize request received with empty input.");
                return BadRequest("Input text cannot be empty.");
            }

            var sanitized = _service.Sanitize(request.Input);

            return Ok(new SanitizeResponse
            {
                Output = sanitized
            });
        }
    }
}