using Adesso.Application.Constants;
using Adesso.Application.Helpers.Validation;
using FluentValidation;

namespace Adesso.Application.Features.Category.Commands.Create;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(i => i.Name)
            .NotNull()
            .WithMessage(Messages.CategoryNameNotNull);

        RuleFor(i => i.Name)
          .MinimumLength(2)
          .WithMessage(Messages.CategoryNameMinLen);

        RuleFor(i => i.Name)
          .MaximumLength(24)
            .WithMessage(Messages.CategoryNameMaxLen);

    }
}
