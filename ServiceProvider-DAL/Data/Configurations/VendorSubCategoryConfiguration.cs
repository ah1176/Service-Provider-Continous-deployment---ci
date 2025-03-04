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
    public class VendorSubCategoryConfiguration : IEntityTypeConfiguration<VendorSubCategory>
    {
        public void Configure(EntityTypeBuilder<VendorSubCategory> builder)
        {
            builder.HasKey(x=> new{ x.VendorId ,x.SubCategoryId});

            builder.HasOne(x => x.SubCategory)
                .WithMany(sc => sc.VendorSubCategories)
                .HasForeignKey(x => x.SubCategoryId)
                .IsRequired();

            builder.HasOne(x => x.Vendor)
                .WithMany(v => v.VendorSubCategories)
                .HasForeignKey(x => x.VendorId)
                .IsRequired();
        }
    }
}
