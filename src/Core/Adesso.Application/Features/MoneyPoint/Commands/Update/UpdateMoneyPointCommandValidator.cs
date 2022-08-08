using Adesso.Application.Constants;
using Adesso.Application.Helpers.Validation;
using FluentValidation;

namespace Adesso.Application.Features.MoneyPoint.Commands.Update;

public class UpdateMoneyPointCommandValidator : AbstractValidatorCustom<UpdateMoneyPointCommand>
{
    public UpdateMoneyPointCommandValidator()
    {
        RuleFor(i => i.Id)
            .NotNull()
            .WithMessage(Messages.MoneyPointNotFound);

        RuleFor(i => i.CategoryId)
            .NotNull()
            .WithMessage(Messages.MoneyPointCategoryIdNotNull);

        RuleFor(i => i.Point)
           .NotNull()
           .WithMessage(Messages.MoneyPointPointNotNull);

        RuleFor(i => i.Point)
          .GreaterThanOrEqualTo(0)
          .WithMessage(Messages.MoneyPointPointMin);

    }
}