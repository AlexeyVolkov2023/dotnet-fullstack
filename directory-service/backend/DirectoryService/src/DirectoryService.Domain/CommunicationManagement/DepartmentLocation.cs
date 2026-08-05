using DirectoryService.Domain.CommunicationManagement.Ids;
using DirectoryService.Domain.DepartmentManagement.Aggregate;
using DirectoryService.Domain.LocationManagement.Ids;

namespace DirectoryService.Domain.CommunicationManagement;

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