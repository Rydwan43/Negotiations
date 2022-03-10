using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Negotiations.Application.Features.Negotiations.Commands.Create;
using Negotiations.Application.Interfaces;
using Negotiations.Domain.Enums;

namespace Negotiations.Application.Features.Negotiations.Validators
{
    public class CreateNegotiationValidator : AbstractValidator<CreateNegotiationCommand>
    {
        private readonly INegotiationsDbContext _dbContex;
        public CreateNegotiationValidator(INegotiationsDbContext dbContext)
        {
            _dbContex = dbContext;

            RuleFor(n => n.CreatedByEmail)
                .NotEmpty()
                .EmailAddress();

            RuleFor(n => n.Price)
                .NotEmpty()
                .Must(p => p > 0)
                .WithMessage("The price must be bigger than zero.")
                .MustAsync(async (entity, value, c) => await VerifyNegotiationPrice(entity, value))
                .WithMessage("The price cannot be twice the base price.");

            RuleFor(n => n.ProductId)
                .NotEmpty()
                .MustAsync(async (entity, value, c) => await VerifyProduct(entity, value))
                .WithMessage("The product is unavailable or does not exist.")
                .MustAsync(async (entity, value, c) => await VerifyNegotiationPrice(entity, value))
                .WithMessage("The price cannot be twice the base price.");
        }
        public async Task<bool> VerifyProduct(CreateNegotiationCommand command, int productId)
        {
            var product  = await _dbContex.Products.FirstOrDefaultAsync(x => x.Id == productId);
            
            if (product is not null && product.Status == ProductStatus.Available)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> VerifyNegotiationTimes(CreateNegotiationCommand command, int productId)
        {
            var countNegotiations  = await _dbContex.Negotiations
                .CountAsync(x => x.ProductId == command.ProductId && x.CreatedByEmail == command.CreatedByEmail);

            if (countNegotiations < 3)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> VerifyNegotiationPrice(CreateNegotiationCommand command, decimal price)
        {
            var product  = await _dbContex.Products.FirstOrDefaultAsync(x => x.Id == command.ProductId);

            if (product is not null && product.BasePrice * 2 >= command.Price)
            {
                return true;
            }

            return false;
        }
    }
}