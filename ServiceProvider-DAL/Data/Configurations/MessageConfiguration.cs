﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceProvider_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_DAL.Data.Configurations
{
    internal class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(m => m.MessageId);

            builder.Property(m => m.MessageText)
                .IsRequired()
                .HasMaxLength(1000);


            builder.Property(m => m.MessageDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");


            builder.Property(m => m.IsRead)
                .IsRequired()
                .HasDefaultValue(false);


            builder.HasOne(m => m.User)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(m => m.Vendor)
                .WithMany(v => v.Messages)
                .HasForeignKey(m => m.VendorId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(m => m.Order)
                .WithMany(o => o.Messages)
                .HasForeignKey(m => m.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
