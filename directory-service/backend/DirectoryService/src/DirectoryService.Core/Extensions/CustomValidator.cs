using System.Text.Json;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace DirectoryService.Core.Extensions;

public static class CustomValidators
{
    public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
        this IRuleBuilder<T, TElement> ruleBuilder,
        Func<TElement, Result<TValueObject>> factoryMethod)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            Result<TValueObject> result = factoryMethod.Invoke(value);

            if (result.IsSuccess)
                return;

            context.AddFailure(JsonSerializer.Serialize(result.Error));
        });
    }
}