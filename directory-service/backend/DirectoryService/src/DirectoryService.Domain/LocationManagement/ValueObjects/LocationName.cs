using DirectoryService.Domain.Shared;

namespace DirectoryService.Domain.LocationManagement.ValueObjects;

public sealed record LocationName
{
    private LocationName(string value)
    {
        Value = value;
    }

    public string Value { get; }


    public static LocationName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Название локации не может быть пустым или содержать пробелы.", nameof(value));
        }

        var normalized = value.Trim();

        if (normalized.Length is < LengthConstants.Length3 or > LengthConstants.Length120)
        {
            throw new ArgumentException(
                $"Название локации должно содержать от {LengthConstants.Length3} до {LengthConstants.Length150}" +
                $" символов.", nameof(value));
        }

        return new LocationName(value);
    }
}