using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System_uznawania_przychodow.Entities;

namespace System_uznawania_przychodow.Configurations;

public class CompanyClientConfiguration : IEntityTypeConfiguration<CompanyClient>
{
    public void Configure(EntityTypeBuilder<CompanyClient> builder)
    {
        builder.ToTable("Company_Clients");
        builder.HasKey(cc => cc.ClientId);
        builder.Property(cc => cc.Krs).HasMaxLength(10);
        builder.Property(cc => cc.CompanyName).HasMaxLength(100);
        builder.HasOne(cc => cc.Client)
            .WithOne(c => c.CompanyClient)
            .HasForeignKey<CompanyClient>(cc => cc.ClientId);

        builder.HasData(new List<CompanyClient>()
        {
            new CompanyClient { ClientId = 6, CompanyName = "ABC Sp. z o.o.", Krs = "0000123456" },
            new CompanyClient { ClientId = 7, CompanyName = "XYZ S.A.", Krs = "0000234567" },
            new CompanyClient { ClientId = 8, CompanyName = "Tech Solutions Sp. z o.o.", Krs = "0000345678" },
            new CompanyClient { ClientId = 9, CompanyName = "MegaCorp S.A.", Krs = "0000456789" },
            new CompanyClient { ClientId = 10, CompanyName = "GlobalTech Sp. z o.o.", Krs = "0000567890" }
        });
    }
}