using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Negotiations.Application.Exceptions;
using Negotiations.Application.Interfaces;

namespace Negotiations.Application.Features.Products.Commands.Delete
{
    public class DeleteProductCommand : IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, int>
    {
        private readonly INegotiationsDbContext _dbContext;
        private readonly IUserContextService _userContextService;
        public DeleteProductCommandHandler(INegotiationsDbContext dbContext, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
        }
        public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products
                .Include(x => x.Negotiations)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            
            if (product is null)
            {
                throw new NotFoundException("No product");
            }

            if (product.CreatedByID != _userContextService.GetUserId)
            {
                throw new UnauthorizedAccessException();
            }

            _dbContext.Negotiations.RemoveRange(product.Negotiations);
            _dbContext.Products.Remove(product);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}