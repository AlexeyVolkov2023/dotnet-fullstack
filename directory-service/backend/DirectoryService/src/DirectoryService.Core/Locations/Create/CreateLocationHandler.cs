using DirectoryService.Domain.Locations.Aggregate;
using DirectoryService.Domain.Locations.ValueObjects;
using FluentValidation;

namespace DirectoryService.Core.Locations.Create;

public class CreateLocationHandler
{
    private readonly ILocationRepository _locationRepository;
    private readonly IValidator<CreateLocationCommand> _validator;

    public CreateLocationHandler(
        ILocationRepository locationRepository,
        IValidator<CreateLocationCommand> validator)
    {
        _locationRepository = locationRepository;
        _validator = validator;
    }

    public async Task<Guid> Handle(
        CreateLocationCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var locationName = LocationName.Create(command.CreateLocationDto.Name);

        var locationExist = await _locationRepository.DoesLocationNameExistExcludingIdAsync(
            locationName,
            cancellationToken);
        if (locationExist!)
        {
            throw new InvalidOperationException(
                $"Location with name '{command.CreateLocationDto.Name}' already exists.");
        }

        var address = Address.Create(
            command.CreateLocationDto.Address.Country,
            command.CreateLocationDto.Address.Region,
            command.CreateLocationDto.Address.City,
            command.CreateLocationDto.Address.Street,
            command.CreateLocationDto.Address.HouseNumber);

        var locationToSave = Location.Create(locationName, address);

        var locationResult = await _locationRepository.AddAsync(locationToSave, cancellationToken);

        return locationResult;
    }
}