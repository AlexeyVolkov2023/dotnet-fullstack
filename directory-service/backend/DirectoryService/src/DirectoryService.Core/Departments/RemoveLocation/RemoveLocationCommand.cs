namespace DirectoryService.Core.Departments.RemoveLocation;

public record RemoveLocationCommand(Guid DepartmentId, Guid LocationId);