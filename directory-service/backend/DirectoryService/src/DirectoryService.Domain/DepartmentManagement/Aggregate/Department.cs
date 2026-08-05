using DirectoryService.Domain.CommunicationManagement;
using DirectoryService.Domain.CommunicationManagement.Ids;
using DirectoryService.Domain.DepartmentManagement.ValueObjects;
using DirectoryService.Domain.LocationManagement.Ids;
using Path = DirectoryService.Domain.DepartmentManagement.ValueObjects.Path;
using Slug = DirectoryService.Domain.DepartmentManagement.ValueObjects.Slug;

namespace DirectoryService.Domain.DepartmentManagement.Aggregate;

public sealed class Department
{
    private readonly List<DepartmentLocation> _departmentLocations = [];

    private readonly List<DepartmentPosition> _departmentPositions = [];
    
    private Department()
    {
    }

    private Department(
        DepartmentName departmentName,
        Slug slug,
        Path path,
        Department? parent,
        IEnumerable<Guid> locationIds)
    {
        Id = Guid.CreateVersion7();
        DepartmentName = departmentName;
        Slug = slug;
        Path = path;
        Parent = parent;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        var locations = locationIds.Select(locationId =>
                new DepartmentLocation(
                    DepartmentLocationId.NewDepartmentLocationId(),
                    this,
                    LocationId.Create(locationId)))
            .ToList();

        _departmentLocations = locations;

        _departmentPositions = new List<DepartmentPosition>();
    }

    public Guid Id { get; private set; }

    public DepartmentName DepartmentName { get; private set; } = null!;

    public Slug Slug { get; private set;} = null!;

    public Path Path { get; private set; } = null!;

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public Department? Parent { get; private set; }
    
    public IReadOnlyList<DepartmentLocation> DepartmentLocations => _departmentLocations;
    
    public IReadOnlyList<DepartmentPosition> DepartmentPositions => _departmentPositions;

    public static Department Create(
        DepartmentName departmentName,
        Slug slug,
        Department? parent,
        IEnumerable<Guid> locationIds)
    {
        var path = BuildPath(parent, slug);

        return new Department(
            departmentName,
            slug,
            path,
            parent,
            locationIds);
    }

    private static Path BuildPath(Department? parent, Slug slug)
    {
        var pathStr = parent?.Slug.Value ?? string.Empty;

        if (!string.IsNullOrEmpty(pathStr))
        {
            pathStr += ".";
        }

        pathStr += slug.Value;

        return Path.Create(pathStr);
    }
}