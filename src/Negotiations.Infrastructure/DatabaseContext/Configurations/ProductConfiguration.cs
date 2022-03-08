using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Negotiations.Domain.Entities;

namespace Negotiations.Infrastructure.DatabaseContext.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.BasePrice)
                .IsRequired()
                .HasPrecision(19,4);
                
            builder.Property(p => p.CreatedByID)
                .IsRequired();
        }
    }
}