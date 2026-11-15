using System.Diagnostics.CodeAnalysis;
using DirectoryService.Domain.Locations.Aggregate;
using DirectoryService.Domain.Locations.Ids;
using DirectoryService.Domain.Locations.ValueObjects;

namespace DirectoryService.Core.Locations;

public interface ILocationRepository
{
    Task<LocationId> AddAsync(Location location, CancellationToken cancellationToken = default);

    Task<bool> ExistsByNameAsync(
        LocationName locationName,
        CancellationToken cancellationToken = default);
    
    Task<bool> ExistByIdAsync(
        LocationId locationId,
        CancellationToken cancellationToken = default);
    
    Task<bool> DoesLocationNameExistExcludingIdAsync(
        LocationName locationName,
        LocationId excludedId,
        CancellationToken cancellationToken = default);
    
    Task<Location> GetByIdAsync(
        LocationId locationId,
        CancellationToken cancellationToken = default);
    
    Task Save(CancellationToken cancellationToken);
}