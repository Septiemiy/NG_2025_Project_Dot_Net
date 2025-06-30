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
    public class CommandConfiguration : IEntityTypeConfiguration<Command>
    {
        public void Configure(EntityTypeBuilder<Command> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.DeviceId)
                .IsRequired();

            builder.Property(c => c.CommandName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Status)
                .IsRequired();

            builder.Property(c => c.Role)
                .IsRequired();
        }
    }
}
