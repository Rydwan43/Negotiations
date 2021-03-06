using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Negotiations.Application.Exceptions;
using Negotiations.Application.Interfaces;
using Negotiations.Domain.Enums;

namespace Negotiations.Application.Features.Negotiations.Commands.Update
{
    public class AcceptNegotiationCommand : IRequest<int>
    {
        public int ProductId { get; set; }
        public int Id { get; set; }
    }

    public class AcceptNegotiationCommandHandler : IRequestHandler<AcceptNegotiationCommand, int>
    {
        private readonly INegotiationsDbContext _dbContext;
        private readonly IUserContextService _userContextService;
        public AcceptNegotiationCommandHandler(INegotiationsDbContext dbContext, 
        IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
        }

        public async Task<int> Handle(AcceptNegotiationCommand request, CancellationToken cancellationToken)
        {
            var negotiation = await _dbContext.Negotiations.FirstOrDefaultAsync(x => x.Id == request.Id);
            
            if (negotiation.ProductId != request.ProductId)
            {
                throw new NotFoundException("The negotiation does not belong to this product");
            }

            var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == negotiation.ProductId);

            if(product.CreatedByID != _userContextService.GetUserId)
            {
                throw new UnauthorizedAccessException();
            }

            product.BasePrice = negotiation.Price;
            negotiation.Status = NegotiationStatus.Accepted;
            negotiation.LastModified = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return negotiation.Id;
        }
    }
}