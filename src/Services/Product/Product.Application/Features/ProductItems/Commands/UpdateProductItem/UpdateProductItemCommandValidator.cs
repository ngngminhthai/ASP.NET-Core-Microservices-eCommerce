﻿using FluentValidation;

namespace Product.Application.Features.ProductItems.Commands.UpdateProductItem
{
    public class UpdateProductItemCommandValidator : AbstractValidator<UpdateProductItemCommand>
    {
        public UpdateProductItemCommandValidator()
        {
            RuleFor(x => x.ProductItem).NotNull();
            RuleFor(x => x.ProductItem.Id).GreaterThan(0).WithMessage("ProductItem Id must be greater than zero");
            // Add more validation rules as needed
        }
    }
}
