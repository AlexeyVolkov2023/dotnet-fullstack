using FluentValidation;

namespace DirectoryService.Core.Departments.Communications.AddLocation;

public class AddLocationCommandValidator: AbstractValidator<AddLocationCommand>
{
    public AddLocationCommandValidator()
    {
        RuleFor(x => x.DepartmentId)
            .NotEmpty();

        RuleFor(x => x.LocationId)
            .NotEmpty();
    }
}