namespace DirectoryService.Contracts.Location;

public record UpdateLocationDto(
    string Name,
    AddressDto Address);