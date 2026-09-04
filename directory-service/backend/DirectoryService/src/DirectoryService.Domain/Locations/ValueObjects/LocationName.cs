using DirectoryService.Domain.Shar;

namespace DirectoryService.Domain.Locations.ValueObjects;

public sealed record LocationName
{
    private LocationName(string value)
    {
        Value = value;
    }

    public string Value { get; }


    public static LocationName Create(string value)
    {
        var normalized = value?.Trim() ?? string.Empty;

        if (normalized.Length is < LengthConstants.Length3 or > LengthConstants.Length120)
        {
            throw new ArgumentException(
                $"Название локации должно содержать от {LengthConstants.Length3} до {LengthConstants.Length120} " +
                $"символов и не может быть пустым. Текущая длина: {normalized.Length}", nameof(value));
        }

        return new LocationName(normalized);
    }
}