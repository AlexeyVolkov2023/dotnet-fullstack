using DirectoryService.Domain.Locations.Ids;
using DirectoryService.Domain.Locations.ValueObjects;

namespace DirectoryService.Domain.Locations.Aggregate;

public sealed class Location
{
    private Location()
    {
    }

    private Location(
        LocationName locationName,
        Address address)
    {
        Id = LocationId.NewLocationId();
        LocationName = locationName;
        Address = address;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public LocationId? Id { get; private set; }

    public LocationName LocationName { get; private set; } = null!;

    public Address Address { get; private set; } = null!;

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public static Location Create(
        LocationName locationName,
        Address address)
    {
        return new Location(locationName, address);
    }
}