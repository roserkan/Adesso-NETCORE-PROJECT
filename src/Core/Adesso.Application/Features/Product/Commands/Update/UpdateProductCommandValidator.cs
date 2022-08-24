﻿using Adesso.Application.Constants;
using Adesso.Application.Helpers.Validation;
using FluentValidation;


namespace Adesso.Application.Features.Product.Commands.Update;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(i => i.Id)
           .NotNull()
           .WithMessage(Messages.ProductIdNotNull);

        RuleFor(i => i.Name)
            .NotNull()
            .WithMessage(Messages.ProductNameNotNull);

        RuleFor(i => i.Name)
          .MinimumLength(2)
          .WithMessage(Messages.ProductNameMinLen);

        RuleFor(i => i.Name)
          .MaximumLength(100)
            .WithMessage(Messages.ProductNameMaxLen);


        RuleFor(i => i.Price)
         .NotNull()
         .WithMessage(Messages.ProductPriceNotNull);

        RuleFor(i => i.Price)
         .GreaterThanOrEqualTo(1)
         .WithMessage(Messages.ProductPriceMin);


        RuleFor(i => i.CategoryId)
        .NotNull()
        .WithMessage(Messages.ProductCategoryIdNotNull);

        RuleFor(i => i.ImagePath)
       .MaximumLength(300)
       .WithMessage(Messages.ProductImagePathMaxLen);

        RuleFor(i => i.Stock)
        .NotNull()
        .WithMessage(Messages.ProductStockNotNull);

        RuleFor(i => i.Stock)
        .GreaterThanOrEqualTo(0)
        .WithMessage(Messages.ProductStockMin);
    }
}

    