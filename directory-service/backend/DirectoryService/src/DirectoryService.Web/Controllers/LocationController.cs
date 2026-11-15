using DirectoryService.Contracts.Locations;
using DirectoryService.Core.Locations.Create;
using DirectoryService.Core.Locations.Update;
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
        var command = new CreateLocationCommand(request);

        var id = await handler.Handle(command, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id }, new { Id = id });
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

    [HttpPatch("{locationId:guid}")]
    public async Task<IActionResult> UpdateDetails(
        [FromRoute] Guid locationId,
        [FromServices] UpdateLocationHandler handler,
        [FromBody] UpdateLocationDto request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateLocationCommand(locationId, request);

        await handler.Handle(command, cancellationToken);

        return Ok(new { Id = locationId });
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        return Ok();
    }
}