using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Negotiations.Domain.Entities;

namespace Negotiations.Infrastructure.DatabaseContext
{
    public class NegotiationsContext : DbContext {
        public NegotiationsContext() { }
        public NegotiationsContext(DbContextOptions<NegotiationsContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users {get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Negotiation> Negotiations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
            base.OnModelCreating(builder);
        }
    }   
}