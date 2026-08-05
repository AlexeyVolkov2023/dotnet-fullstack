namespace DirectoryService.Domain.LocationManagement.ValueObjects;

public sealed record Address
{
    private Address(
        string country,
        string region,
        string city,
        string street,
        string houseNumber)
    {
        Country = country;
        Region = region;
        City = city;
        Street = street;
        HouseNumber = houseNumber;
    }

    public string Street { get; }

    public string HouseNumber { get; }

    public string City { get; }

    public string Region { get; }

    public string Country { get; }


    public static Address Create(
        string country,
        string region,
        string city,
        string street,
        string houseNumber)
    {
        var countryNormalized = country.Trim();

        if (string.IsNullOrWhiteSpace(countryNormalized))
        {
            throw new ArgumentException("Название страны не может быть пустым или содержать пробелы.",
                nameof(country));
        }

        var regionNormalized = region?.Trim();
        if (string.IsNullOrWhiteSpace(regionNormalized))
        {
            throw new ArgumentException("Название региона не может быть пустым.", nameof(region));
        }

        var cityNormalized = city?.Trim();
        if (string.IsNullOrWhiteSpace(cityNormalized))
        {
            throw new ArgumentException("Название города не может быть пустым.", nameof(city));
        }

        var streetNormalized = street?.Trim();
        if (string.IsNullOrWhiteSpace(streetNormalized))
        {
            throw new ArgumentException("Название улицы не может быть пустым.", nameof(street));
        }

        var houseNumberNormalized = houseNumber?.Trim();
        if (string.IsNullOrWhiteSpace(houseNumberNormalized))
        {
            throw new ArgumentException("Номер дома не может быть пустым.", nameof(houseNumber));
        }

        return new Address(
            countryNormalized,
            regionNormalized,
            cityNormalized,
            streetNormalized,
            houseNumberNormalized);
    }
}