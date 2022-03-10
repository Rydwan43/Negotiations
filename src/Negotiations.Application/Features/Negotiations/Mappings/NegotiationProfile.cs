using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Negotiations.Application.Features.Negotiations.Commands.Create;
using Negotiations.Domain.Entities;

namespace Negotiations.Application.Features.Negotiations.Mappings
{
    public class NegotiationProfile : Profile
    {
        public NegotiationProfile()
        {
            CreateMap<CreateNegotiationCommand, Negotiation>();
        }
    }
}