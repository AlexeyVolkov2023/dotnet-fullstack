using DirectoryService.Domain.Communications.Ids;
using DirectoryService.Domain.Departments.Aggregate;
using DirectoryService.Domain.Locations.Ids;

namespace DirectoryService.Domain.Communications;

public record DepartmentLocation
{
    private DepartmentLocation()
    {
        DepartmentLocationId = null!;
        Department = null!;
        LocationId = null!;
    }

    public DepartmentLocation(
        DepartmentLocationId departmentLocationId,
        Department department,
        LocationId locationId)
    {
        DepartmentLocationId = departmentLocationId;
        Department = department;
        LocationId = locationId;
        CreatedAt = DateTime.UtcNow;
    }

    public DepartmentLocationId DepartmentLocationId { get; }

    public Department Department { get; }

    public LocationId LocationId { get; }

    public DateTime CreatedAt { get; }
}