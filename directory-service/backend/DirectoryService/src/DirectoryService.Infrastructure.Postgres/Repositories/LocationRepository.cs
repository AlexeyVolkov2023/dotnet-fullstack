using DirectoryService.Core.Locations;
using DirectoryService.Domain.Locations.Aggregate;
using DirectoryService.Domain.Locations.Ids;
using DirectoryService.Domain.Locations.ValueObjects;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS8603 // Possible null reference return.

namespace DirectoryService.Infrastructure.Postgres.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly DirectoryServiceDbContext _dbContext;

    public LocationRepository(DirectoryServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<LocationId> AddAsync(Location location, CancellationToken cancellationToken)
    {
        await _dbContext.Locations.AddAsync(location, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return location.Id;
    }

    public async Task<bool> DoesLocationNameExistExcludingIdAsync(
        LocationName locationName,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Locations
            .AnyAsync(l => l.LocationName == locationName, cancellationToken);
    }
}