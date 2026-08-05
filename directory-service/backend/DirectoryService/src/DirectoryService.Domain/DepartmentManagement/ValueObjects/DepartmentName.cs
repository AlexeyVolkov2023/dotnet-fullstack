using DirectoryService.Domain.Shared;

namespace DirectoryService.Domain.DepartmentManagement.ValueObject;

public sealed record DepartmentName
{
    private DepartmentName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static DepartmentName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Название отдела не может быть пустым или содержать пробелы.", nameof(value));
        }

        var normalized = value.Trim();

        if (normalized.Length is < LengthConstants.Length3 or > LengthConstants.Length150)
        {
            throw new ArgumentException(
                $"Название отдела должно содержать от {{LengthConstants.Length3}} до {{LengthConstants.Length150}}" +
                $" символов.", nameof(value));
        }

        return new DepartmentName(normalized);
    }
}