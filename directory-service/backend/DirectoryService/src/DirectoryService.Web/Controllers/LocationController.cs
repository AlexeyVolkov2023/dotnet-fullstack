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
        // Маппинг DTO в Command
        var command = new CreateLocationCommand(request);
        
        // Делегируем работу хендлеру. Исключения будут всплывать дальше.
        var id = await handler.Handle(command, cancellationToken);

        // Если исключений не было, возвращаем успешный ответ
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