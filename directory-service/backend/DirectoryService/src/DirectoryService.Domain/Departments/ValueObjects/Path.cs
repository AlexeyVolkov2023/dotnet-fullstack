using DirectoryService.Domain.Shar;

namespace DirectoryService.Domain.Departments.ValueObjects;

public sealed partial record Path
{
    private Path(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Path Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Путь не может быть пустым или состоять из пробелов.", nameof(value));
        }

        if (!MyRegex.IsMatch(value))
        {
            throw new ArgumentException(
                "Путь содержит недопустимые символы.Буквы допускаются только в нижнем регистре, после первой буквы" +
                "допускаются буквы, цифры, точки и дефисы", nameof(value));
        }

        if (value.Length > LengthConstants.Length1000)
        {
            throw new ArgumentException("Длина пути не должна превышать 1000 символов.", nameof(value));
        }

        return new Path(value);
    }

    [System.Text.RegularExpressions.GeneratedRegex(@"^[a-z][a-z0-9.-]*$")]
    private static partial System.Text.RegularExpressions.Regex MyRegex { get; }
}