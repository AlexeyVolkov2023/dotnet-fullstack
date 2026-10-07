using DirectoryService.Domain.Departments.Aggregate;
using DirectoryService.Domain.Departments.Ids;
using DirectoryService.Domain.Departments.ValueObjects;
using FluentValidation;

namespace DirectoryService.Core.Departments;

public class CreateDepartmentHandler
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IValidator<CreateDepartmentCommand> _validator;


    public CreateDepartmentHandler(
        IDepartmentRepository departmentRepository,
        IValidator<CreateDepartmentCommand> validator)
    {
        _departmentRepository = departmentRepository;
        _validator = validator;
    }

    public async Task<Guid> Handle(
        CreateDepartmentCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var slug = Slug.Create(command.CreateDepartmentDto.Slug);

        var slugExist = await _departmentRepository.GetBySlugAsync(slug, cancellationToken);

        if (slugExist)
        {
            throw new InvalidOperationException(
                $"Department with slug '{command.CreateDepartmentDto.Slug}' already exists.");
        }

        Department? parent = null;
        string? parentPath = null;

        if (command.CreateDepartmentDto.ParentId.HasValue)
        {
            var parentId = DepartmentId.Create(command.CreateDepartmentDto.ParentId.Value);
    
            
            parent = await _departmentRepository.GetByIdAsync(parentId, cancellationToken);
    
            if (parent is null)
            {
                throw new InvalidOperationException(
                    $"Parent department with id '{command.CreateDepartmentDto.ParentId}' not found.");
            }

            parentPath = parent.Path.Value;
        }

        var departmentName = DepartmentName.Create(command.CreateDepartmentDto.Name);

        var locationIds = command.CreateDepartmentDto.LocationIds?.ToList() ?? [];

        if (locationIds.Count > 0)
        {
            var allLocationsExist = await _departmentRepository.AllLocationsExistAsync(locationIds, cancellationToken);

            if (!allLocationsExist)
            {
                throw new InvalidOperationException("One or more locations were not found.");
            }
        }

        var departmentToCreate = Department.Create(
            departmentName,
            slug,
            parent?.Id,
            parentPath,
            locationIds);

        try
        {
            await _departmentRepository.AddAsync(departmentToCreate, cancellationToken);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            throw new InvalidOperationException("Failed to save department", ex);
        }

        await _departmentRepository.Save(cancellationToken);

        return departmentToCreate.Id ?? throw new InvalidOperationException();
    }
}