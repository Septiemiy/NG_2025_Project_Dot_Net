using DAL_Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Core.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            
            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);
            
            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Role)
                .IsRequired();
        }
    }
}
