using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Negotiations.Domain.Entities;

namespace Negotiations.Infrastructure.DatabaseContext.Configurations
{
    public class NegotiationConfiguration : IEntityTypeConfiguration<Negotiation>
    {
        public void Configure(EntityTypeBuilder<Negotiation> builder)
        {
            builder.Property(n => n.CreatedByEmail)
                .IsRequired();
            
            builder.Property(n => n.CreatedAt)
                .IsRequired();
            
            builder.Property(n => n.Status)
                .IsRequired();
            
            builder.Property(n => n.Price)
                .IsRequired()
                .HasPrecision(19,4);
        }
    }
}