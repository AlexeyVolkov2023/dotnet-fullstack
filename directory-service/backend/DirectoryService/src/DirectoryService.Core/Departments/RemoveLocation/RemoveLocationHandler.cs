using DirectoryService.Core.Locations;
using DirectoryService.Domain.Departments.Ids;
using DirectoryService.Domain.Locations.Ids;
using FluentValidation;

namespace DirectoryService.Core.Departments.RemoveLocation;

public class RemoveLocationHandler
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly IValidator<RemoveLocationCommand> _validator;

    public RemoveLocationHandler(
        IDepartmentRepository departmentRepository,
        ILocationRepository locationRepository,
        IValidator<RemoveLocationCommand> validator)
    {
        _departmentRepository = departmentRepository;
        _locationRepository = locationRepository;
        _validator = validator;
    }

    public async Task<Guid> Handle(
        RemoveLocationCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var departmentExists = await _departmentRepository.GetByIdWithIncludeAsync
        (command.DepartmentId,
            cancellationToken);
        if (departmentExists is null)
        {
            throw new InvalidOperationException($"Department with id '{command.DepartmentId}' not found.");
        }

        var locationExists = await _locationRepository.ExistByIdAsync(
            command.LocationId,
            cancellationToken);
        if (!locationExists)
        {
            throw new InvalidOperationException($"Location with id '{command.LocationId}' not found.");
        }

        var locationId = LocationId.Create(command.LocationId);
        var departmentId = DepartmentId.Create(command.DepartmentId);

        var existingLink = departmentExists.DepartmentLocations
            .FirstOrDefault(dl => dl.LocationId == locationId && dl.Department.Id == departmentId);
        if (existingLink == null)
        {
            throw new InvalidOperationException("Communication not found");
        }
        
        try
        {
            departmentExists.RemoveLocationFromDepartment(existingLink.DepartmentLocationId);

            await _departmentRepository.Save(cancellationToken);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            throw new InvalidOperationException("Failed to remove location link", ex);
        }

        return existingLink.DepartmentLocationId.Value;
    }
}

