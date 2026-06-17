using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System_uznawania_przychodow.Entities;

namespace System_uznawania_przychodow.Configurations;

public class SoftwareDiscountConfiguration : IEntityTypeConfiguration<SoftwareDiscount>
{
    public void Configure(EntityTypeBuilder<SoftwareDiscount> builder)
    {
        builder.ToTable("Software_Discounts");
        builder.HasKey(sd => new { sd.SoftwareId, sd.DiscountId });
        builder.HasOne(sd => sd.Software)
            .WithMany(s => s.SoftwareDiscounts)
            .HasForeignKey(sd => sd.SoftwareId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(sd => sd.Discount)
            .WithMany(d => d.SoftwareDiscounts)
            .HasForeignKey(sd => sd.DiscountId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(new List<SoftwareDiscount>()
        {
            new SoftwareDiscount { SoftwareId = 1, DiscountId = 3 },
            new SoftwareDiscount { SoftwareId = 1, DiscountId = 4 },
            new SoftwareDiscount { SoftwareId = 1, DiscountId = 1 },
            new SoftwareDiscount { SoftwareId = 2, DiscountId = 3 },
            new SoftwareDiscount { SoftwareId = 2, DiscountId = 2 },
            new SoftwareDiscount { SoftwareId = 3, DiscountId = 4 }
        });
    }
}