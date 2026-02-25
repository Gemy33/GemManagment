using GemManagment.DAL.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.DAL.Data.Configuration
{
    internal class GemUserConfig<T> : IEntityTypeConfiguration<T> where T : GemUser
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(u => u.Name)
                    .HasColumnType("Varchar")
                    .HasMaxLength(50);
            builder.Property(u => u.Email)
                    .HasColumnType("Varchar")
                    .HasMaxLength(100);
            builder.Property(u => u.Phone)
                    .HasColumnType("Varchar")
                    .HasMaxLength(11);

            builder.OwnsOne(u => u.Address, AddressBuilder =>
            {
                AddressBuilder.Property(a => a.Street)
                        .HasColumnType("Varchar")
                        .HasMaxLength(50);
                        //.HasColumnName("Street");
                AddressBuilder.Property(a => a.City)
                        .HasColumnType("Varchar")
                        .HasMaxLength(50);
                //.HasColumnName("City");
                AddressBuilder.Property(a => a.BuildingNumber);
                        //.HasColumnName("BuildingNumber");


            });

            builder.ToTable( tb => {
                tb.HasCheckConstraint("EmailCheckFormat", "Email like '%@%._%'");
                tb.HasCheckConstraint("PhoneCheckFormat", "LEN(Phone) = 11 AND Phone NOT LIKE '%[^0-9]%'");
            });

            builder.HasIndex(u => u.Email).IsUnique();  // non-clustered index unique
            builder.HasIndex(u => u.Phone).IsUnique();  // non-clustered index unique

            


        }
    }
}
