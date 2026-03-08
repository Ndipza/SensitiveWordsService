using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SensitiveWords.Application.DTOs.Sanitization;
using SensitiveWords.Application.Interfaces;
using System.Text.Json;

namespace SensitiveWords.Api.Controllers
{
    /// <summary>
    /// Handles text sanitization by masking sensitive words.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/sanitizer")]
    [EnableRateLimiting("SanitizePolicy")]
    public class SanitizerController : ControllerBase
    {
        private readonly ISanitizationService _service;
        private readonly ILogger<SanitizerController> _logger;

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
        /// <param name="request">Input text payload</param>
        /// <returns>Sanitized text</returns>
        [HttpPost]
        [ProducesResponseType(typeof(SanitizeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<SanitizeResponse> Sanitize([FromBody] SanitizeRequest request)
        {
            if (request == null)
            {
                _logger.LogWarning("Sanitize request received with null payload.");
                return BadRequest("Request body cannot be null.");
            }

            _logger.LogInformation("Sanitization request received. Input length: {Length}", request.Input?.Length ?? 0);

            if (string.IsNullOrWhiteSpace(request.Input))
            {
                _logger.LogWarning("Sanitize request received with empty or whitespace input.");
                return BadRequest("Input text cannot be empty or whitespace.");
            }

            var result = _service.Sanitize(request.Input);

            // Workaround: serialize manually to avoid System.Text.Json writing directly to PipeWriter used by TestServer
            var responseObj = new SanitizeResponse { Output = result };
            var json = JsonSerializer.Serialize(responseObj);

            return Content(json, "application/json");
        }
    }
}