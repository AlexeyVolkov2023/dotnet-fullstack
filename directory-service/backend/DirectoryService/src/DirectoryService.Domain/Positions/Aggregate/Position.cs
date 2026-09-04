using DirectoryService.Domain.Positions.Ids;
using DirectoryService.Domain.Positions.ValueObjects;

namespace DirectoryService.Domain.Positions.Aggregate;

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