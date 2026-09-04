using DirectoryService.Domain.Communications.Ids;
using DirectoryService.Domain.Departments.Aggregate;
using DirectoryService.Domain.Positions.Ids;

namespace DirectoryService.Domain.Communications;

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