using Negotiations.Domain.Common;

namespace Negotiations.Domain.Entities
{
    public class Product : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual List<Negotiation> Negotiations { get; set; }
    }
}