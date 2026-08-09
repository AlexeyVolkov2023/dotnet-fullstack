using DirectoryService.Domain.PositionManagement.Ids;
using DirectoryService.Domain.PositionManagement.ValueObjects;

namespace DirectoryService.Domain.PositionManagement.Aggregate;

public class Position
{
    public Position()
    {
    }

    private Position(PositionName positionName)
    {
        Id = PositionId.NewPositionId();
        PositionName = positionName;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public PositionId? Id { get; private set; }

    public PositionName PositionName { get; private set; } = null!;

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public static Position Create(PositionName positionName)
    {
        return new Position(positionName);
    }
}