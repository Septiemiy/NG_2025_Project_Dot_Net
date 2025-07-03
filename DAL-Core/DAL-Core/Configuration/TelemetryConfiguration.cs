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
    public class TelemetryConfiguration : IEntityTypeConfiguration<Telemetry>
    {
        public void Configure(EntityTypeBuilder<Telemetry> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.DeviceId)
                .IsRequired();

            builder.Property(t => t.DataType)
                .IsRequired();

            builder.Property(t => t.DataValue)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
