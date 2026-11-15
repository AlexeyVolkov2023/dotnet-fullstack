using DirectoryService.Contracts.Departments;
using DirectoryService.Core.Departments;
using DirectoryService.Core.Departments.Communications.AddLocation;
using DirectoryService.Core.Departments.RemoveLocation;
using DirectoryService.Core.Departments.UpdateName;
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
    public async Task<IActionResult> Create(
        [FromServices] CreateDepartmentHandler handler,
        [FromBody] CreateDepartmentDto request,
        CancellationToken cancellationToken)
    {
        var command = new CreateDepartmentCommand(request);

        var id = await handler.Handle(command, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id }, new { Id = id });
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        return Ok();
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(Array.Empty<object>());
    }

    [HttpPatch("{departmentId:guid}")]
    public async Task<IActionResult> UpdateName(
        [FromRoute] Guid departmentId,
        [FromServices] UpdateDepartmentNameHandler handler,
        [FromBody] UpdateDepartmentNameDto request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateDepartmentNameCommand(departmentId, request);

        await handler.Handle(command, cancellationToken);

        return Ok(new { Id = departmentId });
    }

    [HttpPost("/api/departments/{departmentId:guid}/locations/{locationId:guid}")]
    public async Task<IActionResult> AddLocationToDepartment(
        [FromRoute] Guid departmentId,
        [FromRoute] Guid locationId,
        [FromServices] AddLocationHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new AddLocationCommand(departmentId, locationId);

        await handler.Handle(command, cancellationToken);

        return Ok(new { Id = departmentId});
    }

    [HttpDelete("/api/departments/{departmentId:guid}/locations/{locationId:guid}")]
    public async Task<IActionResult> RemoveLocationToDepartment(
        [FromRoute] Guid departmentId,
        [FromRoute] Guid locationId,
        [FromServices] RemoveLocationHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new RemoveLocationCommand(departmentId, locationId);

        await handler.Handle(command, cancellationToken);

        return Ok(new { Id = departmentId});
    }
}