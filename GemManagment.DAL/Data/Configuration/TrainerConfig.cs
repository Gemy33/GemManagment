using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.DAL.Data.Configuration
{
    internal class TrainerConfig: GemUserConfig<Models.Trainer>, IEntityTypeConfiguration<Models.Trainer>
    {
        public new void Configure(EntityTypeBuilder<Models.Trainer> builder)
        {
            builder.Property(t => t.CreatedAt)
                    .HasColumnName("HireDate")
                    .HasDefaultValueSql("GETDATE()");
            base.Configure(builder);
            
        }
    }

}
