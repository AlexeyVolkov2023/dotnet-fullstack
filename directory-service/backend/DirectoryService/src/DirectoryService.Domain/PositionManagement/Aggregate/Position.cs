using DirectoryService.Domain.PositionManagement.ValueObjects;

namespace DirectoryService.Domain.PositionManagement.Aggregate;

public class Position
{
    public Position()
    {
    }

    private Position(PositionName positionName)
    {
        Id = Guid.CreateVersion7();
        PositionName = positionName;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }

    public PositionName PositionName { get; private set; } = null!;

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public static Position Create(PositionName positionName)
    {
        return new Position(positionName);
    }
}