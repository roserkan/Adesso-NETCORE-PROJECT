using Adesso.Application.Constants;
using Adesso.Application.Helpers.Validation;
using FluentValidation;

namespace Adesso.Application.Features.Commands.UserDetail.Update;

public class UpdateUserDetailCommandValidator : AbstractValidatorCustom<UpdateUserDetailCommand>
{
    public UpdateUserDetailCommandValidator()
    {
        RuleFor(i => i.Id)
           .NotNull()
           .WithMessage(Messages.UserDetailNotFound);

        RuleFor(i => i.UserId)
            .NotNull()
            .WithMessage(Messages.UserUserIdNotNull);


        RuleFor(i => i.FirstName)
            .NotNull()
            .WithMessage(Messages.UserFirstNameNotNull);

        RuleFor(i => i.FirstName)
           .MinimumLength(2)
           .WithMessage(Messages.UserFirstNameMinLen);

        RuleFor(i => i.FirstName)
           .MaximumLength(24)
           .WithMessage(Messages.UserFirstNameMaxLen);



        RuleFor(i => i.LastName)
            .NotNull()
            .WithMessage(Messages.UserLastNameNotNull);

        RuleFor(i => i.LastName)
           .MinimumLength(2)
           .WithMessage(Messages.UserLastNameMinLen);

        RuleFor(i => i.FirstName)
           .MaximumLength(24)
           .WithMessage(Messages.UserLastNameMaxLen);

        RuleFor(i => i.Address)
            .NotNull()
            .WithMessage(Messages.UserAddressNotNull);

        RuleFor(i => i.Address)
           .MinimumLength(2)
           .WithMessage(Messages.UserAddressMinLen);

        RuleFor(i => i.Address)
           .MaximumLength(500)
           .WithMessage(Messages.UserAddressMaxLen);

    }
}
