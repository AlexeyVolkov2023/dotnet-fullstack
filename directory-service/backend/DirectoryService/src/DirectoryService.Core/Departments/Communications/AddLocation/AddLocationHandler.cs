using DirectoryService.Core.Locations;
using DirectoryService.Domain.Departments.Ids;
using DirectoryService.Domain.Locations.Ids;
using FluentValidation;

namespace DirectoryService.Core.Departments.Communications.AddLocation;

public class AddLocationHandler
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly IValidator<AddLocationCommand> _validator;

    public AddLocationHandler(
        IDepartmentRepository departmentRepository,
        ILocationRepository locationRepository,
        IValidator<AddLocationCommand> validator)
    {
        _departmentRepository = departmentRepository;
        _locationRepository = locationRepository;
        _validator = validator;
    }

    public async Task<Guid> Handle(
        AddLocationCommand command,
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

        if (departmentExists.DepartmentLocations.Any(dl => dl.LocationId == locationId))
        {
            throw new InvalidOperationException($"Location is exist");
        }

        try
        {
            departmentExists.AddLocationToDepartment(command.LocationId);

            await _departmentRepository.Save(cancellationToken);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            throw new InvalidOperationException("Failed to save location link", ex);
        }

        return command.DepartmentId;
    }
}