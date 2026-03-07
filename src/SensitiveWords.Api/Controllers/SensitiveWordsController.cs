using Microsoft.AspNetCore.Mvc;
using SensitiveWords.Application.DTOs.SensitiveWords;
using SensitiveWords.Application.Interfaces;
using SensitiveWords.Application.Services;

namespace SensitiveWords.Api.Controllers
{
    [ApiController]
    [Route("api/sensitive-words")]
    public class SensitiveWordsController : ControllerBase
    {
        private readonly ISensitiveWordService _service;

        public SensitiveWordsController(ISensitiveWordService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all sensitive words
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensitiveWordResponse>>> GetAll()
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }

        /// <summary>
        /// Add a new sensitive word
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSensitiveWordRequest request)
        {
            await _service.AddAsync(request);

            return Created(string.Empty, null);
        }

        /// <summary>
        /// Update an existing sensitive word
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] UpdateSensitiveWordRequest request)
        {
            await _service.UpdateAsync(id, request);

            return NoContent();
        }

        /// <summary>
        /// Delete a sensitive word
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}
