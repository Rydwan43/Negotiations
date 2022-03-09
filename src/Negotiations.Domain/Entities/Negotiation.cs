using Negotiations.Domain.Enums;

namespace Negotiations.Domain.Entities
{
    public class Negotiation
    {
        public int Id { get; set; }
        public string CreatedByEmail { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModified { get; set; }
        public NegotiationStatus Status { get; set; }
    }
}