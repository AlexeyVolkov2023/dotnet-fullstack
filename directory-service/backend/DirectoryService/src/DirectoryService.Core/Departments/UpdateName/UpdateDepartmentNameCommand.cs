using DirectoryService.Contracts.Departments;

namespace DirectoryService.Core.Departments.UpdateName;

public record UpdateDepartmentNameCommand(Guid DepartmentId, UpdateDepartmentNameDto UpdateDepartmentNameDto);