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
    internal class MemberSessionConfig : IEntityTypeConfiguration<MemberSession>
    {
        public void Configure(EntityTypeBuilder<MemberSession> builder)
        {
            builder.Property(m => m.CreatedAt)
                   .HasDefaultValueSql("GETDATE()")
                   .HasColumnName("BookingDay");
            builder.HasKey(b => new { b.SessionId, b.MemberId });
            builder.Ignore(b => b.Id);
        }
    }
}
