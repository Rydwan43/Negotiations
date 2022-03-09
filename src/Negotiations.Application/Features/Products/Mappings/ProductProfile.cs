using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Negotiations.Application.Features.Products.Commands.Create;
using Negotiations.Domain.Entities;

namespace Negotiations.Application.Features.Products.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductCommand, Product>();
        }
    }
}