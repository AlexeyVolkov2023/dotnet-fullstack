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
}