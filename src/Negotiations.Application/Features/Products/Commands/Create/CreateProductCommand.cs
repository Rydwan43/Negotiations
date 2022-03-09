using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Negotiations.Application.Interfaces;
using Negotiations.Domain.Entities;

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
        public CreateProductCommandHandler(INegotiationsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var newProduct = _mapper.Map<Product>(request);
            
            newProduct.CreatedByID = 1;

            _dbContext.Products.Add(newProduct);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return newProduct.Id;
        }
    }
}