using Adesso.Application.CrossCuttingConcerns.Exceptions;
using FluentValidation;

namespace Adesso.Application.Helpers.Validation;

public abstract class AbstractValidatorCustom<T> : AbstractValidator<T>
{

    public override FluentValidation.Results.ValidationResult Validate(ValidationContext<T> context)
    {
        var validationResult = base.Validate(context);

        if (!validationResult.IsValid)
        {
            throw new BusinessException(validationResult.Errors[0].ToString());
            //RaiseValidationException(context, validationResult);
        }


        return validationResult;
    }
}