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
    internal class PlanConfig : IEntityTypeConfiguration<Plans>
    {
        public void Configure(EntityTypeBuilder<Plans> builder)
        {
            builder.Property(p => p.Name)
                    .HasColumnType("Varchar")
                    .HasMaxLength(50);
            builder.Property(p => p.Description)
                   .HasColumnType("Varchar")
                   .HasMaxLength(100);
            builder.Property(p => p.Price)
                   .HasColumnType("Decimal(8,2)");

            builder.ToTable(tb => tb.HasCheckConstraint("PlanMaxDaysCheck", "DurationInDays BETWEEN 1 AND 365"));
           
        }
    }
}
