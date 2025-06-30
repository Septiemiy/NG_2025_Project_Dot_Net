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
    public class TriggerConfiguration : IEntityTypeConfiguration<Trigger>
    {
        public void Configure(EntityTypeBuilder<Trigger> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.DeviceId)
                .IsRequired();

            builder.Property(t => t.TriggerName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.TriggerCondition)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(t => t.Action)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(t => t.Role)
                .IsRequired();
        }
    }
}
