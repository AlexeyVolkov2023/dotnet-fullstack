using DirectoryService.Core.Departments;
using DirectoryService.Domain.Communications;
using DirectoryService.Domain.Departments.Aggregate;
using DirectoryService.Domain.Departments.Ids;
using DirectoryService.Domain.Departments.ValueObjects;
using DirectoryService.Domain.Locations.Ids;
using Microsoft.EntityFrameworkCore;

namespace DirectoryService.Infrastructure.Postgres.Repositories;

public class EfCoreDepartmentRepository : IDepartmentRepository
{
    private readonly DirectoryServiceDbContext _dbContext;

    public EfCoreDepartmentRepository(DirectoryServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> AddAsync(Department department, CancellationToken cancellationToken)
    {
        await _dbContext.Departments.AddAsync(department, cancellationToken);

        if (department.Id is null)
        {
            throw new InvalidOperationException("Department do not save");
        }

        return department.Id;
    }

    public async Task<bool> GetBySlugAsync(Slug slug, CancellationToken cancellationToken)
    {
        var department = await _dbContext.Departments
            .FirstOrDefaultAsync(d => d.Slug.Value == slug.Value, cancellationToken);

        if (department is null)
        {
            return false;
        }

        return true;
    }

    public async Task<Department> GetByIdAsync(DepartmentId departmentId, CancellationToken cancellationToken)
    {
        var department = await _dbContext.Departments
            .FirstOrDefaultAsync(d => d.Id == departmentId, cancellationToken);

        if (department is null)
        {
            throw new InvalidOperationException(
                $"Department with id '{departmentId}' not found.");
        }

        return department;
    }
    
    public async Task<Department> GetByIdWithIncludeAsync(DepartmentId departmentId, CancellationToken cancellationToken)
    {
        var department = await _dbContext.Departments
            .Include(l => l.DepartmentLocations)
            .FirstOrDefaultAsync(d => d.Id == departmentId , cancellationToken);

        if (department is null)
        {
            throw new InvalidOperationException(
                $"Department with id '{departmentId}' not found.");
        }

        return department;
    }


    public async Task<bool> AllLocationsExistAsync(
        IEnumerable<Guid> locationIds,
        CancellationToken cancellationToken)
    {
        var locationIdsList = locationIds.ToList();

        if (locationIdsList.Count == 0)
        {
            return true;
        }

        var existingCount = await _dbContext.Locations
            .Where(l => locationIdsList.Contains(l.Id!))
            .Select(l => l.Id)
            .Distinct()
            .CountAsync(cancellationToken);

        return existingCount == locationIdsList.Distinct().Count();
    }

    public async Task<bool> IsLocationLinkedAsync(
        DepartmentId departmentId,
        LocationId locationId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<DepartmentLocation>()
            .AnyAsync(dl =>
                    dl.Department.Id != null &&
                    dl.Department.Id.Value == departmentId.Value &&
                    dl.LocationId.Value == locationId.Value,
                cancellationToken);
    }

    public async Task Save(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}