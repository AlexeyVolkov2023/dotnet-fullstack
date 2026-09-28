using DirectoryService.Core.Locations;
using DirectoryService.Domain.Locations.Aggregate;
using DirectoryService.Domain.Locations.Ids;
using DirectoryService.Domain.Locations.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace DirectoryService.Infrastructure.Postgres.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly DirectoryServiceDbContext _dbContext;

    public LocationRepository(DirectoryServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<LocationId> AddAsync(Location location, CancellationToken cancellationToken = default)
    {
        await _dbContext.Locations.AddAsync(location, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        if (location.Id is null)
        {
            throw new InvalidOperationException("Location do not save");
        }

        return location.Id;
    }

    public async Task<bool> ExistsByNameAsync(LocationName locationName, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Locations
            .AnyAsync(l => l.LocationName.Value == locationName.Value, cancellationToken);
    }
}