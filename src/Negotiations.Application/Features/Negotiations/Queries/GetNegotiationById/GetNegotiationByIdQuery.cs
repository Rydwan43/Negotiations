using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Negotiations.Application.Exceptions;
using Negotiations.Application.Interfaces;
using Negotiations.Domain.Entities;

namespace Negotiations.Application.Features.Negotiations.Queries.GetNegotiationById
{
    public class GetNegotiationByIdQuery : IRequest<Negotiation>
    {
        public int ProductId { get; set; }
        public int Id { get; set; }
    }

    public class GetNegotiationByIdQueryHandler : IRequestHandler<GetNegotiationByIdQuery, Negotiation>
    {
        private readonly INegotiationsDbContext _dbContext;
        public GetNegotiationByIdQueryHandler(INegotiationsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Negotiation> Handle(GetNegotiationByIdQuery request, CancellationToken cancellationToken)
        {
            var negotiation = await _dbContext.Negotiations
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.ProductId == request.ProductId, cancellationToken);
            
            if (negotiation is null)
                throw new NotFoundException("Negotiation not found");

            return negotiation;
        }
    }
}