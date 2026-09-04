using DirectoryService.Domain.Shar;

namespace DirectoryService.Domain.Locations.ValueObjects;

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
    var countryNormalized = country?.Trim();
    if (string.IsNullOrWhiteSpace(countryNormalized))
    {
        throw new ArgumentException("Название страны не может быть пустым.", nameof(country));
    }
    if (countryNormalized.Length > LengthConstants.Length100)
    {
        throw new ArgumentException(
            $"Название страны не должно превышать {LengthConstants.Length100} символов." +
            $" Текущая длина: {countryNormalized.Length}.", nameof(country));
    }

    var regionNormalized = region?.Trim();
    if (string.IsNullOrWhiteSpace(regionNormalized))
    {
        throw new ArgumentException("Название региона не может быть пустым.", nameof(region));
    }
    if (regionNormalized.Length > LengthConstants.Length100)
    {
        throw new ArgumentException(
            $"Название региона не должно превышать {LengthConstants.Length100} символов." +
            $" Текущая длина: {regionNormalized.Length}.", nameof(region));
    }

    var cityNormalized = city?.Trim();
    if (string.IsNullOrWhiteSpace(cityNormalized))
    {
        throw new ArgumentException("Название города не может быть пустым.", nameof(city));
    }
    if (cityNormalized.Length > LengthConstants.Length100)
    {
        throw new ArgumentException(
            $"Название города не должно превышать {LengthConstants.Length100} символов." +
            $" Текущая длина: {cityNormalized.Length}.", nameof(city));
    }

    var streetNormalized = street?.Trim();
    if (string.IsNullOrWhiteSpace(streetNormalized))
    {
        throw new ArgumentException("Название улицы не может быть пустым.", nameof(street));
    }
    if (streetNormalized.Length > LengthConstants.Length250)
    {
        throw new ArgumentException(
            $"Название улицы не должно превышать {LengthConstants.Length250} символов." +
            $" Текущая длина: {streetNormalized.Length}.", nameof(street));
    }

    var houseNumberNormalized = houseNumber?.Trim();
    if (string.IsNullOrWhiteSpace(houseNumberNormalized))
    {
        throw new ArgumentException("Номер дома не может быть пустым.", nameof(houseNumber));
    }
    if (houseNumberNormalized.Length > LengthConstants.Length20)
    {
        throw new ArgumentException(
            $"Номер дома не должен превышать {LengthConstants.Length20} символов." +
            $" Текущая длина: {houseNumberNormalized.Length}.", nameof(houseNumber));
    }

    return new Address(
        countryNormalized,
        regionNormalized,
        cityNormalized,
        streetNormalized,
        houseNumberNormalized);
}
}