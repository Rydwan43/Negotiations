using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Negotiations.Application.Exceptions;
using Negotiations.Application.Interfaces;
using Negotiations.Domain.Entities;
using Negotiations.Domain.Enums;

namespace Negotiations.Application.Features.Negotiations.Commands.Create;

public class CreateNegotiationCommand : IRequest<int>
{
    public string CreatedByEmail { get; set; }
    public decimal Price { get; set; }
    public int ProductId { get; set; }
}

public class CreateNegotiationCommandHandler : IRequestHandler<CreateNegotiationCommand, int>
{
    private readonly INegotiationsDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateNegotiationCommandHandler(INegotiationsDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateNegotiationCommand request, CancellationToken cancellationToken)
    {
        var product = _dbContext.Products.FirstOrDefault(x => x.Id == request.ProductId);
        var newNegotiation = _mapper.Map<Negotiation>(request);
        
        newNegotiation.CreatedAt = DateTime.UtcNow;
        newNegotiation.Status = NegotiationStatus.Pending;

        await _dbContext.Negotiations.AddAsync(newNegotiation, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return newNegotiation.Id;
    }
}