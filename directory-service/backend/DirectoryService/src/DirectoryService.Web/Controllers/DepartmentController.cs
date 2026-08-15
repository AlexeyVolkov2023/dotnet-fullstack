using DirectoryService.Contracts.Department;
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
public sealed class DepartmentController : ControllerBase
{
    [HttpPost]
    public IActionResult Create([FromBody] CreateDepartmentDto request)
    {
        var newId = Guid.NewGuid();
        return CreatedAtAction(nameof(GetById), new { id = newId }, new { Id = newId });
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        return NotFound();
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(Array.Empty<object>());
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateDepartmentDto request)
    {
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        return Ok();
    }
}