using DirectoryService.Core.Extensions;
using DirectoryService.Domain.Departments.ValueObjects;
using DirectoryService.Domain.Shar;
using FluentValidation;

namespace DirectoryService.Core.Departments;

public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidator()
    {
        RuleFor(x => x.CreateDepartmentDto.Name)
            .NotEmpty()
            .MaximumLength(LengthConstants.Length120)
            .MinimumLength(LengthConstants.Length3)
            .WithMessage(
                $"Название отдела должно содержать от {LengthConstants.Length3} до {LengthConstants.Length120} " +
                $"символов и не может быть пустым.");

        RuleFor(x => x.CreateDepartmentDto.Slug)
            .NotEmpty()
            .MaximumLength(LengthConstants.Length150)
            .MinimumLength(LengthConstants.Length3)
            .WithMessage("Слаг должен содержать от {LengthConstants.Length3} до {LengthConstants.Length150} символов.");
    }
}