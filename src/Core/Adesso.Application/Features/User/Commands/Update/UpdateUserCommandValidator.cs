using Adesso.Application.Constants;
using Adesso.Application.Helpers.Validation;
using FluentValidation;

namespace Adesso.Application.Features.User.Commands.Update;

public class UpdateUserCommandValidator : AbstractValidatorCustom<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {

        RuleFor(i => i.Id)
        .NotNull()
        .WithMessage(Messages.ProductIdNotNull);

        RuleFor(i => i.EmailAddress)
            .NotNull()
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
            .WithMessage(Messages.UserEmailAddressNotValid);


        RuleFor(i => i.Password)
            .NotNull()
            .MinimumLength(6)
            .WithMessage(Messages.UserPasswordMinLen);

        RuleFor(i => i.Password)
            .NotNull()
            .MaximumLength(16)
            .WithMessage(Messages.UserPasswordMaxLen);

    }
}
