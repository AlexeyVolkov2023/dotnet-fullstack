using DirectoryService.Domain.Shar;
using FluentValidation;

namespace DirectoryService.Core.Locations.Create;

public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
{
    public CreateLocationCommandValidator()
    {
        RuleFor(x => x.CreateLocationDto.Name)
            .NotEmpty()
            .MaximumLength(LengthConstants.Length120)
            .MinimumLength(LengthConstants.Length3)
            .WithMessage(
                $"Название локации должно содержать от {LengthConstants.Length3} до {LengthConstants.Length120} " +
                $"символов и не может быть пустым.");

        RuleFor(x => x.CreateLocationDto.Address.Country)
            .NotEmpty()
            .MaximumLength(LengthConstants.Length100)
            .WithMessage($"Название страны не может быть пустым и должно содержать" +
                         $" не более {LengthConstants.Length100} символов.");

        RuleFor(x => x.CreateLocationDto.Address.Region)
            .NotEmpty()
            .MaximumLength(LengthConstants.Length100)
            .WithMessage($"Название региона не может быть пустым и должно содержать" +
                         $" не более {LengthConstants.Length100} символов.");

        RuleFor(x => x.CreateLocationDto.Address.City)
            .NotEmpty()
            .MaximumLength(LengthConstants.Length100)
            .WithMessage($"Название города не может быть пустым и должно содержать" +
                         $" не более {LengthConstants.Length100} символов.");

        RuleFor(x => x.CreateLocationDto.Address.Street)
            .NotEmpty()
            .MaximumLength(LengthConstants.Length250)
            .WithMessage($"Название улицы не может быть пустым и должно содержать" +
                         $" не более {LengthConstants.Length250} символов.");

        RuleFor(x => x.CreateLocationDto.Address.HouseNumber)
            .NotEmpty()
            .MaximumLength(LengthConstants.Length20)
            .WithMessage($"Номер дома не может быть пустым и должен содержать" +
                         $" не более {LengthConstants.Length20} символов.");

        RuleFor(x => x.CreateLocationDto.Address.ApartmentNumber)
            .MaximumLength(LengthConstants.Length20)
            .WithMessage($"Номер квартиры должен содержать не более {LengthConstants.Length20} символов.");
    }
}