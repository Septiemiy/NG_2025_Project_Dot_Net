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
    public class ThresholdConfiguration : IEntityTypeConfiguration<Threshold>
    {
        public void Configure(EntityTypeBuilder<Threshold> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.DeviceId)
                .IsRequired();

            builder.Property(t => t.ThresholdName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Condition)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Action)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Status)
                .IsRequired();
        }
    }
}
