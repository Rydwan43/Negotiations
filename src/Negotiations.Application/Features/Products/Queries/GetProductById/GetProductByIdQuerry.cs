using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Negotiations.Application.Interfaces;
using Negotiations.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Negotiations.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQuerry : IRequest<Product>
    {
        public int Id { get; set; }
    }

    public class GetProductByIdQuerryHandler : IRequestHandler<GetProductByIdQuerry, Product>
        {
            private readonly INegotiationsDbContext _dbContext;

            public GetProductByIdQuerryHandler(INegotiationsDbContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<Product> Handle(GetProductByIdQuerry request, CancellationToken cancellationToken)
            {   
                var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (product is null)
                {
                    throw new NotImplementedException();
                }

                return await Task.FromResult(product);
            }
        }
}