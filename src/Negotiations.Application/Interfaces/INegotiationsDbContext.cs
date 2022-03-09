using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Negotiations.Domain.Entities;

namespace Negotiations.Application.Interfaces
{
    public interface INegotiationsDbContext
    {
        public DbSet<User> Users {get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Negotiation> Negotiations { get; set; }        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}