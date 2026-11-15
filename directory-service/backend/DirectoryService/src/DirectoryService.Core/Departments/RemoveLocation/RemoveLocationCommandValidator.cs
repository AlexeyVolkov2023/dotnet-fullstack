using FluentValidation;

namespace DirectoryService.Core.Departments.RemoveLocation;

public class RemoveLocationCommandValidator : AbstractValidator<RemoveLocationCommand>
{
    public RemoveLocationCommandValidator()
    {
        RuleFor(x => x.DepartmentId)
            .NotEmpty();

        RuleFor(x => x.LocationId)
            .NotEmpty();
    }
}