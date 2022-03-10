using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Negotiations.Application.Features.Products.Commands.Create;

namespace Negotiations.Application.Features.Products.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.BasePrice)
                .NotEmpty()
                .Must(x => x > 0)
                .WithMessage("The Base Price must be bigger than 0");
        }
    }
}