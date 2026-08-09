using DirectoryService.Domain.Shar;

namespace DirectoryService.Domain.DepartmentManagement.ValueObjects;

public sealed record DepartmentName
{
    private DepartmentName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static DepartmentName Create(string value)
    {
        var normalized = value?.Trim() ?? string.Empty;

        if (normalized.Length is < LengthConstants.Length3 or > LengthConstants.Length150)
        {
            throw new ArgumentException(
                $"Название отдела должно содержать от {LengthConstants.Length3} до {LengthConstants.Length150} " +
                $"символов и не может быть пустым. Текущая длина: {normalized.Length}", nameof(value));
        }

        return new DepartmentName(normalized);
    }
}