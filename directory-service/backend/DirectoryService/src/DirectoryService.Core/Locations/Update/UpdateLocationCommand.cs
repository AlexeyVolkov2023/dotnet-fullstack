using DirectoryService.Contracts.Locations;

namespace DirectoryService.Core.Locations.Update;

public record UpdateLocationCommand(Guid LocationId, UpdateLocationDto UpdateLocationDto);