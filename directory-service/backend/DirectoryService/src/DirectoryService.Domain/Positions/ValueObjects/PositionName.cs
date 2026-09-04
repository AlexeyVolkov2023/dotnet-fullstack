using DirectoryService.Domain.Shar;

namespace DirectoryService.Domain.Positions.ValueObjects;

public sealed record PositionName
{
    private PositionName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static PositionName Create(string value)
    {
        var normalized = value?.Trim() ?? string.Empty;

        if (normalized.Length is < LengthConstants.Length3 or > LengthConstants.Length100)
        {
            throw new ArgumentException(
                $"Название позиции должно содержать от {LengthConstants.Length3} до {LengthConstants.Length100} " +
                $"символов и не может быть пустым. Текущая длина: {normalized.Length}", nameof(value));
        }

        return new PositionName(normalized);
    }
}