using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Negotiations.Application.Exceptions;
using Negotiations.Application.Interfaces;
using Negotiations.Domain.Enums;

namespace Negotiations.Application.Features.Products.Commands.Update
{
    public class UpdateProductCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductStatus? Status { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
    {
        private readonly INegotiationsDbContext _dbContext;
        private readonly IUserContextService _userContextService;
        public UpdateProductCommandHandler(INegotiationsDbContext dbContext, 
        IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;    
        }

        public async Task<int> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == command.Id);

            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }

            if (product.CreatedByID != _userContextService.GetUserId)
            {
                throw new UnauthorizedAccessException();
            }

            product.Name = command.Name ?? product.Name;
            product.Description = command.Description ?? product.Description;
            product.Status = (command.Status != null) ? (ProductStatus)command.Status : product.Status;

            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}