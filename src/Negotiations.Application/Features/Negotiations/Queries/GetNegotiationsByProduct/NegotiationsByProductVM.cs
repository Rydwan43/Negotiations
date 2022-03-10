using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Negotiations.Domain.Entities;

namespace Negotiations.Application.Features.Negotiations.Queries.GetNegotiationsByProduct
{
    public class NegotiationsByProductVM
    {
        public IEnumerable<Negotiation> AcceptedNegotations { get; set; }
        public IEnumerable<Negotiation> RejectedNegotiations { get; set; }
        public IEnumerable<Negotiation> PendingNegotiations { get; set; }
    }
}