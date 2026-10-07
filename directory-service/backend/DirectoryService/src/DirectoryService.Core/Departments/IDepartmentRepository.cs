using DirectoryService.Domain.Departments.Aggregate;
using DirectoryService.Domain.Departments.Ids;
using DirectoryService.Domain.Departments.ValueObjects;

namespace DirectoryService.Core.Departments;

public interface IDepartmentRepository
{
    Task<Guid> AddAsync(Department department, CancellationToken cancellationToken);

    Task<bool> GetBySlugAsync(Slug slug, CancellationToken cancellationToken);

    Task<Department> GetByIdAsync(DepartmentId departmentId, CancellationToken cancellationToken);

    Task<bool> AllLocationsExistAsync(IEnumerable<Guid> locationIds, CancellationToken cancellationToken);

    Task Save(CancellationToken cancellationToken);
}