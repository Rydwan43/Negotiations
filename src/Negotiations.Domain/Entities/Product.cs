using System.Text.Json.Serialization;
using Negotiations.Domain.Common;
using Negotiations.Domain.Enums;

namespace Negotiations.Domain.Entities
{
    public class Product : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public ProductStatus Status { get; set; }
        
        [JsonIgnore]
        public virtual List<Negotiation> Negotiations { get; set; }
    }
}