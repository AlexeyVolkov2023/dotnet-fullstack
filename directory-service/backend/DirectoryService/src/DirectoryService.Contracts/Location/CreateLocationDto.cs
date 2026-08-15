namespace DirectoryService.Contracts.Location;

public record CreateLocationDto(
    string Name,
    AddressDto Address);

