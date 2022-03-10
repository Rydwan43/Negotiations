using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Negotiations.Application.Interfaces;
using Negotiations.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Negotiations.Application.Exceptions;
using Negotiations.Domain.Enums;

namespace Negotiations.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQuerry : IRequest<ProductByIdVM>
    {
        public int Id { get; set; }
    }

    public class GetProductByIdQuerryHandler : IRequestHandler<GetProductByIdQuerry, ProductByIdVM>
        {
            private readonly INegotiationsDbContext _dbContext;

            public GetProductByIdQuerryHandler(INegotiationsDbContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<ProductByIdVM> Handle(GetProductByIdQuerry request, CancellationToken cancellationToken)
            {   
                var product = await _dbContext.Products
                .Include(r => r.Negotiations)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (product is null)
                {
                    throw new NotFoundException("Product not found");
                }

                var negotiationsAccepted = product.Negotiations
                .Where(x => x.Status == NegotiationStatus.Accepted)
                .OrderBy(x => x.Price);

                var negotiationsRejected = product.Negotiations.Where(x => x.Status == NegotiationStatus.Rejected)
                .OrderBy(x => x.Price);

                var negotiationsPending = product.Negotiations.Where(x => x.Status == NegotiationStatus.Pending)
                .OrderBy(x => x.Price);
                
                var productVM = new ProductByIdVM {
                    CurrentProduct = product,
                    AcceptedNegotations = negotiationsAccepted,
                    PendingNegotiations = negotiationsPending,
                    RejectedNegotiations = negotiationsRejected
                };

                return await Task.FromResult(productVM);
            }
        }
}