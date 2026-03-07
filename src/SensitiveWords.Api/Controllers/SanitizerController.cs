using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SensitiveWords.Application.DTOs.Sanitization;
using SensitiveWords.Application.Services;

namespace SensitiveWords.Api.Controllers
{
    [ApiController]
    [Route("api/sanitizer")]
    [EnableRateLimiting("SanitizePolicy")]
    public class SanitizerController : ControllerBase
    {
        private readonly SanitizationService _service;
        private readonly ILogger<SanitizerController> _logger;

        public SanitizerController(
            SanitizationService service,
            ILogger<SanitizerController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Sanitizes input text by masking sensitive words.
        /// </summary>
        /// <remarks>
        /// Example request:
        ///
        ///     POST /api/sanitizer
        ///     {
        ///         "input": "SELECT * FROM USERS"
        ///     }
        ///
        /// Example response:
        ///
        ///     {
        ///         "output": "****** * **** USERS"
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        public ActionResult<SanitizeResponse> Sanitize([FromBody] SanitizeRequest request)
        {
            var result = _service.Sanitize(request.Input);

            return Ok(new SanitizeResponse
            {
                Output = result
            });
        }
    }
}