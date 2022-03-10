using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Negotiations.Application.Features.Negotiations.Commands.Update;
using Negotiations.Application.Interfaces;
using Negotiations.Domain.Enums;

namespace Negotiations.Application.Features.Negotiations.Validators
{
    public class AcceptNegotiationValidator : AbstractValidator<AcceptNegotiationCommand>
    {
        private readonly INegotiationsDbContext _dbContext;
        private readonly IUserContextService _userContextService;
        public AcceptNegotiationValidator(INegotiationsDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(n => n.Id)
                .NotEmpty()
                .Custom((value, context) => {
                    
                    var negotiation = _dbContext.Negotiations.FirstOrDefault(x => x.Id == value);
                    
                    if (negotiation == null)
                        context.AddFailure("Id","Couldn't find Negotiation");
                });

            RuleFor(n => n.ProductId)
                .NotEmpty()
                .Custom((value, context) => {
                    
                    var product = _dbContext.Products.FirstOrDefault(x => x.Id == value);
                    
                    if (product == null || product.Status != ProductStatus.Available)
                        context.AddFailure("ProductId","You cannot negotiate unavailable product");
                });
            
        }
    }
}