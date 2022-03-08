using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Negotiations.Domain.Common
{
    public abstract class AuditableEntity
    {
        public int CreatedByID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModified { get; set; }
        
    }
}