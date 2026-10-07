namespace DirectoryService.Contracts.Locations;

public record CreateLocationDto(
    string LocationName,
    AddressDto Address);

