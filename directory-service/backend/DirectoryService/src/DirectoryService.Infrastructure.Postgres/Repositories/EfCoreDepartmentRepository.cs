using DirectoryService.Core.Departments;
using DirectoryService.Domain.Departments.Aggregate;
using DirectoryService.Domain.Departments.Ids;
using DirectoryService.Domain.Departments.ValueObjects;
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

    
    
    public async Task<bool> AllLocationsExistAsync(
        IEnumerable<Guid> locationIds,
        CancellationToken cancellationToken)
    {
        var locationIdsList = locationIds.ToList();

        if (locationIdsList.Count == 0)
        {
            return false;
        }

        var existingCount = await _dbContext.Locations
            .Where(l => locationIdsList.Contains(l.Id!))
            .Select(l => l.Id)
            .Distinct()
            .CountAsync(cancellationToken);

        return existingCount == locationIdsList.Distinct().Count();
    }

    public async Task Save(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}