namespace DirectoryService.Contracts.Departments;

public record UpdateDepartmentDto(
    string Name,
    string Slug,
    Guid? ParentId,
    IEnumerable<Guid> LocationIds
);