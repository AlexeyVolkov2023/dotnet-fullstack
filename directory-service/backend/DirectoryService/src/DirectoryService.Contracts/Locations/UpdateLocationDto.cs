namespace DirectoryService.Contracts.Locations;

public record UpdateLocationDto(
     string LocationName,
    AddressDto Address);