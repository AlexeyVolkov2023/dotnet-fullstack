using DirectoryService.Domain.Shar;
using FluentValidation;

namespace DirectoryService.Core.Departments.UpdateName;

public class UpdateDepartmentNameCommandValidator: AbstractValidator<UpdateDepartmentNameCommand>
{
    public UpdateDepartmentNameCommandValidator()
    {
        RuleFor(x => x.UpdateDepartmentNameDto.DepartmentName)
            .NotEmpty()
            .MaximumLength(LengthConstants.Length120)
            .MinimumLength(LengthConstants.Length3)
            .WithMessage(
                $"Название отдела должно содержать от {LengthConstants.Length3} до {LengthConstants.Length120} " +
                $"символов и не может быть пустым.");
    }
}