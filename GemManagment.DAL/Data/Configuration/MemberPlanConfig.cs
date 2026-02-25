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
    internal class MemberPlanConfig : IEntityTypeConfiguration<MemberPlan>
    {
        public void Configure(EntityTypeBuilder<MemberPlan> builder)
        {
            builder.Property(mp => mp.CreatedAt)
                   .HasDefaultValueSql("GETDATE()")
                   .HasColumnName("StartDate");
            builder.HasKey(m => new { m.MemberId, m.PlansId });
            builder.Ignore(m => m.Id);

        }
    }
}
