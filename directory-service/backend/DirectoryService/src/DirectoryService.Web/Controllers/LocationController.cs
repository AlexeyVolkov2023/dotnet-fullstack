using DirectoryService.Contracts.Locations;
using DirectoryService.Core.Locations.Create;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Maintainability",
    "CA1515:Consider making public types internal",
    Justification = "Controllers must be public for ASP.NET Core.",
    Scope = "namespaceanddescendants",
    Target = "DirectoryService.Web.Controllers")]

namespace DirectoryService.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class LocationController : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromServices] CreateLocationHandler handler,
        [FromBody] CreateLocationDto request, 
        CancellationToken cancellationToken)
    {
        try
        {
            var command = new CreateLocationCommand(request);
            
            var id = await handler.Handle(command, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id }, new { Id = id });
        }
        catch (ValidationException ex)
        {
           return BadRequest(new 
            { 
                errors = ex.Errors.Select(e => e.ErrorMessage) 
            });
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("already exists", StringComparison.Ordinal))
        {
            // 409 Conflict для бизнес-ошибок (занятое имя)
            return Conflict(new { error = "Conflict", message = ex.Message });
        }
#pragma warning disable CA1031
        catch
#pragma warning restore CA1031
        {
            // 500 Internal Server Error для всего остального
            // В реальном проекте здесь должно быть логирование через ILogger
            return StatusCode(500, new { error = "Internal Error", message = "An unexpected error occurred." });
        }
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(
        [FromRoute] Guid id)
    {
        return NotFound();
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(Array.Empty<object>());
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateLocationDto request)
    {
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        return Ok();
    }
}