namespace DirectoryService.Contracts.Department;

public record UpdateDepartmentDto(
    string Name,
    string Slug,
    Guid? ParentId,
    IEnumerable<Guid> LocationIds
);