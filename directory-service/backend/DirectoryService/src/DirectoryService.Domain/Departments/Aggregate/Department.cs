using DirectoryService.Domain.Communications;
using DirectoryService.Domain.Communications.Ids;
using DirectoryService.Domain.Departments.Ids;
using DirectoryService.Domain.Departments.ValueObjects;
using DirectoryService.Domain.Locations.Ids;
using Path = DirectoryService.Domain.Departments.ValueObjects.Path;
using Slug = DirectoryService.Domain.Departments.ValueObjects.Slug;

namespace DirectoryService.Domain.Departments.Aggregate;

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
        DepartmentId? parentId,
        IEnumerable<Guid> locationIds)
    {
        Id = DepartmentId.NewDepartmentId();
        DepartmentName = departmentName;
        Slug = slug;
        Path = path;
        ParentId = parentId;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        _departmentLocations = locationIds
            .Select(locationId => new DepartmentLocation(
                DepartmentLocationId.NewDepartmentLocationId(),
                this,
                LocationId.Create(locationId)))
            .ToList();

        _departmentPositions = new List<DepartmentPosition>();
    }

    public DepartmentId? Id { get; private set; }

    public DepartmentName DepartmentName { get; private set; } = null!;

    public Slug Slug { get; private set; } = null!;

    public Path Path { get; private set; } = null!;

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public DepartmentId? ParentId { get; private set; }

    public Department? Parent { get; private set; }

    public IReadOnlyList<DepartmentLocation> DepartmentLocations => _departmentLocations;
    public IReadOnlyList<DepartmentPosition> DepartmentPositions => _departmentPositions;

    public static Department Create(
        DepartmentName departmentName,
        Slug slug,
        DepartmentId? parentId,
        string? parentPath,
        IEnumerable<Guid> locationIds)
    {
        var path = BuildPath(parentPath, slug);

        return new Department(
            departmentName,
            slug,
            path,
            parentId,
            locationIds);
    }

    private static Path BuildPath(string? parentPath, Slug slug)
    {
        var pathStr = parentPath ?? string.Empty;
        if (!string.IsNullOrEmpty(pathStr))
        {
            pathStr += ".";
        }

        pathStr += slug.Value;
        return Path.Create(pathStr);
    }
}