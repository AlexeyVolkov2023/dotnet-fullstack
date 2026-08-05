namespace DirectoryService.Domain.PositionManagement.Ids;

public sealed record PositionId
{
    private PositionId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static PositionId NewPositionId() => new(Guid.NewGuid());

    public static PositionId Empty() => new(Guid.Empty);

    public static PositionId Create(Guid id) => new(id);

    public static implicit operator PositionId(Guid id) => new(id);

    public static implicit operator Guid(PositionId positionId)
    {
        ArgumentNullException.ThrowIfNull(positionId);

        return positionId.Value;
    }

    public static PositionId FromGuid(Guid id) => new(id);

    public static Guid ToGuid(PositionId positionId)
    {
        ArgumentNullException.ThrowIfNull(positionId);
        return positionId.Value;
    }
}