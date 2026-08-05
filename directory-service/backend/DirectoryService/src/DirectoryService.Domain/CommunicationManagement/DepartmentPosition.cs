using DirectoryService.Domain.CommunicationManagement.Ids;
using DirectoryService.Domain.DepartmentManagement.Aggregate;
using DirectoryService.Domain.PositionManagement.Ids;

namespace DirectoryService.Domain.CommunicationManagement;

public record DepartmentPosition
{
    private DepartmentPosition()
    {
        DepartmentPositionId = null!;
        Department = null!;
        PositionId = null!;
    }

    public DepartmentPosition(
        DepartmentPositionId departmentPositionId,
        Department department,
        PositionId positionId)
    {
        DepartmentPositionId = departmentPositionId;
        Department = department;
        PositionId = positionId;
        CreatedAt = DateTime.UtcNow;
    }

    public DepartmentPositionId DepartmentPositionId { get; }

    public Department Department { get; }

    public PositionId PositionId { get; }

    public DateTime CreatedAt { get; }
}