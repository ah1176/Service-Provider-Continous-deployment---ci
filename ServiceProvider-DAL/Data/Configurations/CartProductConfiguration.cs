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
           builder.HasKey(x => new {x.CartId ,x.ProductId});

            builder.HasOne(cp => cp.Cart)
                 .WithMany(x => x.CartProducts)
                 .HasForeignKey(cp => cp.CartId)
                 .IsRequired();

            builder.HasOne(cp => cp.Product)
                 .WithMany(x => x.CartProducts)
                 .HasForeignKey(cp => cp.ProductId)
                 .IsRequired();
        }
    }
}
