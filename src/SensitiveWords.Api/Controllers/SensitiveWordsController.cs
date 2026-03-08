using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SensitiveWords.Application.DTOs.SensitiveWords;
using SensitiveWords.Application.Interfaces;

namespace SensitiveWords.Api.Controllers;

/// <summary>
/// Manages CRUD operations for sensitive words.
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
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SensitiveWordResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SensitiveWordResponse>>> GetAll()
    {
        _logger.LogInformation("Retrieving all sensitive words");

        var result = await _service.GetAllAsync();

        return Ok(result);
    }

    /// <summary>
    /// Adds a new sensitive word.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create(CreateSensitiveWordRequest request)
    {
        _logger.LogInformation("Creating sensitive word {Word}", request.Word);

        await _service.AddAsync(request);

        return CreatedAtAction(nameof(GetAll), null);
    }

    /// <summary>
    /// Updates an existing sensitive word.
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdateSensitiveWordRequest request)
    {
        _logger.LogInformation("Updating sensitive word with id {Id}", id);

        await _service.UpdateAsync(id, request);

        return NoContent();
    }

    /// <summary>
    /// Deletes a sensitive word.
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Deleting sensitive word with id {Id}", id);

        await _service.DeleteAsync(id);

        return NoContent();
    }
}