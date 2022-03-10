using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Negotiations.Domain.Entities;

namespace Negotiations.Application.Features.Products.Queries.GetProductById
{
    public class ProductByIdVM
    {
        public Product CurrentProduct { get; set; }

        public IEnumerable<Negotiation> AcceptedNegotations { get; set; }
        public IEnumerable<Negotiation> RejectedNegotiations { get; set; }
        public IEnumerable<Negotiation> PendingNegotiations { get; set; }
    }
}