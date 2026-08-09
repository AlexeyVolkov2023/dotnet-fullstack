using DirectoryService.Domain.Shar;

namespace DirectoryService.Domain.PositionManagement.ValueObjects;

public sealed record PositionName
{
    private PositionName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static PositionName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Название позиции не может быть пустым или содержать пробелы.", nameof(value));
        }

        var normalized = value.Trim();

        if (normalized.Length is < LengthConstants.Length3 or > LengthConstants.Length100)
        {
            throw new ArgumentException(
                $"Название позиции должно содержать от {LengthConstants.Length3} до {LengthConstants.Length100}" +
                $" символов.", nameof(value));
        }

        return new PositionName(value);
    }
}