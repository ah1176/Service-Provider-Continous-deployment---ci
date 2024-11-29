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
    public class CartProductConfiguration : IEntityTypeConfiguration<CartProduct>
    {
        public void Configure(EntityTypeBuilder<CartProduct> builder)
        {
           builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Cart)
                 .WithMany(x => x.CartProducts)
                 .HasForeignKey(x => x.CartId)
                 .IsRequired();

            builder.HasOne(x => x.Product)
                 .WithMany(x => x.CartProducts)
                 .HasForeignKey(x => x.ProductId)
                 .IsRequired();
        }
    }
}
