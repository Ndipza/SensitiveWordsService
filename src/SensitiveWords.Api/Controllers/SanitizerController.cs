using Microsoft.AspNetCore.Mvc;
using SensitiveWords.Application.DTOs;
using SensitiveWords.Application.Services;

namespace SensitiveWords.Api.Controllers
{
    [ApiController]
    [Route("api/sanitizer")]
    public class SanitizerController : ControllerBase
    {
        private readonly SanitizationService _service;

        public SanitizerController(SanitizationService service)
        {
            _service = service;
        }

        /// <summary>
        /// Sanitizes a string by masking sensitive words.
        /// </summary>
        [HttpPost]
        public ActionResult<SanitizeResponse> Sanitize(SanitizeRequest request)
        {
            var result = _service.Sanitize(request.Input);

            return Ok(new SanitizeResponse
            {
                Output = result
            });
        }
    }
}
