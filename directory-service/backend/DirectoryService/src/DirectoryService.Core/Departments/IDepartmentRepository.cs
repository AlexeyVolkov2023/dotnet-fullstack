using DirectoryService.Domain.Departments.Aggregate;
using DirectoryService.Domain.Departments.Ids;
using DirectoryService.Domain.Departments.ValueObjects;
using DirectoryService.Domain.Locations.Ids;

namespace DirectoryService.Core.Departments;

public interface IDepartmentRepository
{
    Task<Guid> AddAsync(Department department, CancellationToken cancellationToken);

    Task<bool> GetBySlugAsync(Slug slug, CancellationToken cancellationToken);

    Task<Department> GetByIdAsync(DepartmentId departmentId, CancellationToken cancellationToken);
    
    Task<Department> GetByIdWithIncludeAsync(DepartmentId departmentId, CancellationToken cancellationToken);

    Task<bool> AllLocationsExistAsync(IEnumerable<Guid> locationIds, CancellationToken cancellationToken);

    Task<bool> IsLocationLinkedAsync(
        DepartmentId departmentId,
        LocationId locationId,
        CancellationToken cancellationToken = default);

    Task Save(CancellationToken cancellationToken);
}