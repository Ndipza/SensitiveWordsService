using Microsoft.AspNetCore.Mvc;
using SensitiveWords.Application.DTOs.SensitiveWords;
using SensitiveWords.Application.Interfaces;

namespace SensitiveWords.Api.Controllers
{
    [ApiController]
    [Route("api/sensitive-words")]
    public class SensitiveWordsController : ControllerBase
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensitiveWordResponse>>> GetAll()
        {
            _logger.LogInformation("Retrieving sensitive words");

            var result = await _service.GetAllAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSensitiveWordRequest request)
        {
            await _service.AddAsync(request);

            return CreatedAtAction(nameof(GetAll), null);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateSensitiveWordRequest request)
        {
            await _service.UpdateAsync(id, request);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}