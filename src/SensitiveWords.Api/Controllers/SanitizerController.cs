using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SensitiveWords.Api.Swagger.Examples;
using SensitiveWords.Application.DTOs.Sanitization;
using SensitiveWords.Application.Interfaces;
using Swashbuckle.AspNetCore.Filters;

namespace SensitiveWords.Api.Controllers;

/// <summary>
/// Handles text sanitization by masking sensitive words.
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
    /// Initializes a new instance of the SanitizerController class with the specified sanitization service and logger.
    /// </summary>
    /// <param name="service">The service used to perform data sanitization operations. Cannot be null.</param>
    /// <param name="logger">The logger used to record diagnostic and operational information for the controller. Cannot be null.</param>
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
    [HttpPost]
    [ProducesResponseType(typeof(SanitizeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public ActionResult<SanitizeResponse> Sanitize([FromBody] SanitizeRequest request)
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