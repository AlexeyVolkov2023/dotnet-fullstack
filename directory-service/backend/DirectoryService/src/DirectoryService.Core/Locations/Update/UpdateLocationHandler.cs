using DirectoryService.Domain.Locations.ValueObjects;
using FluentValidation;

namespace DirectoryService.Core.Locations.Update;

public class UpdateLocationHandler
{
    private readonly ILocationRepository _locationRepository;
    private readonly IValidator<UpdateLocationCommand> _validator;

    public UpdateLocationHandler(
        ILocationRepository locationRepository,
        IValidator<UpdateLocationCommand> validator)
    {
        _locationRepository = locationRepository;
        _validator = validator;
    }

    public async Task<Guid> Handle(
        UpdateLocationCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var locationResult = await _locationRepository.ExistByIdAsync(
            command.LocationId,
            cancellationToken);
        if (!locationResult)
        {
            throw new InvalidOperationException(
                $"Location with locationId '{command.LocationId}' do not found.");
        }

        var locationName = LocationName.Create(command.UpdateLocationDto.LocationName);

        var duplicateExists = await _locationRepository.DoesLocationNameExistExcludingIdAsync(
            locationName,
            command.LocationId,
            cancellationToken);
        if (duplicateExists)
        {
            throw new InvalidOperationException(
                $"Location with location name '{command.UpdateLocationDto.LocationName}' is duplicate.");
        }

        var address = Address.Create(
            command.UpdateLocationDto.Address.Country,
            command.UpdateLocationDto.Address.Region,
            command.UpdateLocationDto.Address.City,
            command.UpdateLocationDto.Address.Street,
            command.UpdateLocationDto.Address.HouseNumber);

        var locationToUpdate = await _locationRepository.GetByIdAsync(
            command.LocationId,
            cancellationToken);


        try
        {
            locationToUpdate.UpdateDetails(locationName, address);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            throw new InvalidOperationException("Failed to update location", ex);
        }

        await _locationRepository.Save(cancellationToken);

        return locationToUpdate.Id ?? throw new InvalidOperationException();
    }
}