using Adesso.Application.Constants;
using Adesso.Application.Helpers.Validation;
using FluentValidation;

namespace Adesso.Application.Features.Commands.Role.Update;

public class UpdateRoleCommandValidator : AbstractValidatorCustom<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(i => i.Id)
          .NotNull()
          .WithMessage(Messages.RoleNotFound);

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
