using DirectoryService.Domain.Shar;

namespace DirectoryService.Domain.Departments.ValueObjects;

public sealed partial record Slug
{
    private Slug(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Slug Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Слаг не может быть пустым или состоять из пробелов.",  nameof(value));
        }

        #pragma warning disable CA1308
        var normalized = value.Trim().ToLowerInvariant().Replace(' ', '-');

        if (normalized.Length is < LengthConstants.Length3 or > LengthConstants.Length150)
        {
            throw new ArgumentException(
                $"Слаг должен содержать от {LengthConstants.Length3} до {LengthConstants.Length150} символов." +
                $"Текущая длина: {normalized.Length}.", nameof(value));
        }

        if (!MyRegex.IsMatch(normalized))
        {
            throw new ArgumentException(
                "Слаг содержит недопустимые символы.Буквы допускаются только в нижнем регистре, после первой буквы" +
                "допускаются буквы, цифры и дефисы", nameof(value));
        }

        return new Slug(normalized);
    }

    [System.Text.RegularExpressions.GeneratedRegex(@"^[a-z][a-z0-9-]*$")]
    private static partial System.Text.RegularExpressions.Regex MyRegex { get; }
}