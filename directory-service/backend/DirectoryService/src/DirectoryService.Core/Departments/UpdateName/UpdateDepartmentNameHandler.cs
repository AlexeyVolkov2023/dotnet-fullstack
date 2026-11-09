using DirectoryService.Domain.Departments.ValueObjects;
using FluentValidation;

namespace DirectoryService.Core.Departments.UpdateName;

public class UpdateDepartmentNameHandler
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IValidator<UpdateDepartmentNameCommand> _validator;

    public UpdateDepartmentNameHandler(
        IDepartmentRepository departmentRepository,
        IValidator<UpdateDepartmentNameCommand> validator)
    {
        _departmentRepository = departmentRepository;
        _validator = validator;
       }

    public async Task<Guid> Handle(
        UpdateDepartmentNameCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var departmentToUpdate = await _departmentRepository.GetByIdAsync(
            command.DepartmentId,
            cancellationToken);
        if (departmentToUpdate is null)
        {
            throw new InvalidOperationException(
                $"Department with id '{command.DepartmentId}' not found.");
        }

        var departmentName = DepartmentName.Create(command.UpdateDepartmentNameDto.DepartmentName);

        try
        {
            departmentToUpdate.UpdateName(departmentName);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            throw new InvalidOperationException("Failed to update department name", ex);
        }

        await _departmentRepository.Save(cancellationToken);

        return departmentToUpdate.Id ?? throw new InvalidOperationException();
    }
}