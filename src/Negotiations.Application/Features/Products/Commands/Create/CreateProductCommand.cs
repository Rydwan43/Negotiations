using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Negotiations.Application.Interfaces;
using Negotiations.Domain.Entities;
using Negotiations.Domain.Enums;

namespace Negotiations.Application.Features.Products.Commands.Create
{
    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly INegotiationsDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        public CreateProductCommandHandler(INegotiationsDbContext dbContext, IMapper mapper, 
        IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var newProduct = _mapper.Map<Product>(request);
            var userId = _userContextService.GetUserId;
            
            newProduct.CreatedByID = userId;
            newProduct.CreatedAt = DateTime.UtcNow;
            newProduct.Status = ProductStatus.Available;

            _dbContext.Products.Add(newProduct);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return newProduct.Id;
        }
    }
}