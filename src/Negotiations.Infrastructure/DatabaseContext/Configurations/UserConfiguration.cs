using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Negotiations.Domain.Entities;

namespace Negotiations.Infrastructure.DatabaseContext.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Login)
                .IsRequired();
            
            builder.Property(u => u.PasswordHash)
                .IsRequired();
            
            builder.Property(u => u.Email)
                .IsRequired();
        }
    }
}