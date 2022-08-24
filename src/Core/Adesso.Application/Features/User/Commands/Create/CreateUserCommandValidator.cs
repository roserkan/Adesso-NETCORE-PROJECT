using Adesso.Application.Constants;
using FluentValidation;

namespace Adesso.Application.Features.User.Commands.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
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