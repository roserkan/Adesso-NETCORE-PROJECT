using Adesso.Application.Constants;
using Adesso.Application.Helpers.Validation;
using FluentValidation;


namespace Adesso.Application.Features.Commands.Role.Create;

public class CreateRoleCommandValidator : AbstractValidatorCustom<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(i => i.RoleName)
            .NotNull()
            .WithMessage(Messages.RoleNameNotNull);

        RuleFor(i => i.RoleName)
            .MinimumLength(2)
            .WithMessage(Messages.RoleNameMinLen);

        RuleFor(i => i.RoleName)
         .MaximumLength(24)
         .WithMessage(Messages.RoleNameMaxLen);

    }
}
