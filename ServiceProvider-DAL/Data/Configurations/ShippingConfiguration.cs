using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceProvider_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_DAL.Data.Configurations
{
    public class ShippingConfiguration : IEntityTypeConfiguration<Shipping>
    {
        public void Configure(EntityTypeBuilder<Shipping> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                .HasMaxLength(200)
                .IsRequired();


            builder.HasOne(x => x.Order)
                .WithOne(o => o.Shipping)
                .HasForeignKey<Shipping>(x => x.OrderId)
                .IsRequired();

        }
    }
}
