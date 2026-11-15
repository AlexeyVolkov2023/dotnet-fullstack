using DirectoryService.Contracts.Departments;

namespace DirectoryService.Core.Departments.Communications.AddLocation;

public record AddLocationCommand(Guid DepartmentId, Guid LocationId);