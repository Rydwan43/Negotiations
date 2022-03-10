using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Negotiations.Application.Interfaces;
using Negotiations.Domain.Entities;
using Negotiations.Domain.Enums;

namespace Negotiations.Application.Features.Negotiations.Queries.GetNegotiationsByProduct
{
    public class GetNegotiationsByProductQuery : IRequest<NegotiationsByProductVM>
    {
        public int ProductId { get; set; }
    }

    public class GetNegotiationsByProductHandler : IRequestHandler<GetNegotiationsByProductQuery, NegotiationsByProductVM>
    {
        private readonly INegotiationsDbContext _dbContext;

        public GetNegotiationsByProductHandler(INegotiationsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<NegotiationsByProductVM> Handle(GetNegotiationsByProductQuery request, CancellationToken cancellationToken)
        {
            var negotiations = await _dbContext.Negotiations
                .Where(x => x.ProductId == request.ProductId)
                .ToListAsync(cancellationToken);
            
            var acceptedNegotiations = negotiations
                .Where(x => x.Status == NegotiationStatus.Accepted)
                .OrderBy(x => x.LastModified);
                
            var rejectedNegotiations = negotiations
                .Where(x => x.Status == NegotiationStatus.Rejected)
                .OrderBy(x => x.LastModified);

            var pendingNegotiations = negotiations
                .Where(x => x.Status == NegotiationStatus.Pending)
                .OrderBy(x => x.LastModified);

            return new NegotiationsByProductVM {
                AcceptedNegotations = acceptedNegotiations,
                RejectedNegotiations = rejectedNegotiations,
                PendingNegotiations = pendingNegotiations
              };
        }
    }
}