using DirectoryService.Domain.LocationManagement.ValueObjects;

namespace DirectoryService.Domain.LocationManagement.Aggregate;

public sealed class Location
{
    private Location()
    {
    }

    private Location(
        LocationName locationName,
        Address address)
    {
        Id = Guid.CreateVersion7();
        LocationName = locationName;
        Address = address;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }

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