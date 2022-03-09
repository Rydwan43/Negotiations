using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Negotiations.Application.Interfaces;
using Negotiations.Domain.Entities;

namespace Negotiations.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<List<Product>>
    {
        
    }

    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<Product>>
    {
        private readonly INegotiationsDbContext _dbContext;
        public GetAllProductsHandler(INegotiationsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products =  await _dbContext.Products.ToListAsync(cancellationToken);
            
            return products;
        }
    }
}