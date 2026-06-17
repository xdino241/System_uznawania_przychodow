using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System_uznawania_przychodow.Entities;

namespace System_uznawania_przychodow.Configurations;

public class DiscountConfiguration: IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.ToTable("Discounts");
        builder.HasKey(d => d.DiscountId);
        builder.Property(d => d.Name).HasMaxLength(50);
        builder.Property(d => d.Value).HasColumnType("decimal(5,2)");

        builder.HasData(new List<Discount>(){
            new Discount { DiscountId = 1, Name = "Black Friday", Value = 15.00m, StartDate = new DateOnly(2026, 11, 23), EndDate = new DateOnly(2026, 11, 30) },
            new Discount { DiscountId = 2, Name = "Cyber Monday", Value = 20.00m, StartDate = new DateOnly(2026, 11, 30), EndDate = new DateOnly(2026, 12, 2) },
            new Discount { DiscountId = 3, Name = "Wiosenna Promocja", Value = 10.00m, StartDate = new DateOnly(2026, 3, 1), EndDate = new DateOnly(2026, 5, 31) },
            new Discount { DiscountId = 4, Name = "Niezwykłe rabaty", Value = 17.50m, StartDate = new DateOnly(2026, 6, 1), EndDate = new DateOnly(2026, 8, 31) }
        });
    }
}