using GemManagment.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.DAL.Data.Configuration
{
    internal class HealthRecordConfig : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            builder.ToTable("Member");
            builder.HasOne<Member>()
                .WithOne(m => m.HealthRecord)
                     .HasForeignKey<HealthRecord>(hr => hr.Id);
            builder.Property(hr => hr.Height)
                .HasColumnType("decimal(7,2)")
                .IsRequired();
            builder.Property(hr => hr.Weight)
                .HasColumnType("decimal(5,2)")
                .IsRequired();
            builder.Ignore(hr => hr.CreatedAt);
            builder.Ignore(hr => hr.UpdatedAt);
        }
    }
}
