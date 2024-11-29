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
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TotalAmount)
                .HasColumnType("decimal")
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion(
                c => c.ToString(),
                c => (PaymentStatus)Enum.Parse(typeof(PaymentStatus), c)
                );

            builder.Property(x => x.PaymentMethod)
                .IsRequired();

            builder.HasOne(x => x.Order)
                .WithOne(x => x.Payment)
                .HasForeignKey<Payment>(x => x.OrderId)
                .IsRequired();


        }
    }
}
