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
    public class RejectNegotiationCommand : IRequest<int>
    {
        public int ProductId { get; set; }
        public int Id { get; set; }
    }

    public class RejectNegotiationCommandHandler : IRequestHandler<RejectNegotiationCommand, int>
    {
        private readonly INegotiationsDbContext _dbContext;
        private readonly IUserContextService _userContextService;
        public RejectNegotiationCommandHandler(INegotiationsDbContext dbContext, 
        IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
        }

        public async Task<int> Handle(RejectNegotiationCommand request, CancellationToken cancellationToken)
        {
            var negotiation = await _dbContext.Negotiations.FirstOrDefaultAsync(x => x.Id == request.Id);
            var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == negotiation.ProductId);
            
            if(product.CreatedByID != _userContextService.GetUserId)
            {
                throw new UnauthorizedAccessException();
            }

            negotiation.Status = NegotiationStatus.Rejected;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return negotiation.Id;
        }
    }
}