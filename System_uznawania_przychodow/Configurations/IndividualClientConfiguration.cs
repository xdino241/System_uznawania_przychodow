using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System_uznawania_przychodow.Entities;

namespace System_uznawania_przychodow.Configurations;

public class IndividualClientConfiguration : IEntityTypeConfiguration<IndividualClient>
{
    public void Configure(EntityTypeBuilder<IndividualClient> builder)
    {
        builder.ToTable("Individual_Clients");
        builder.HasKey(cc => cc.ClientId);
        builder.Property(ic => ic.FirstName).HasMaxLength(50);
        builder.Property(ic => ic.LastName).HasMaxLength(100);
        builder.Property(ic => ic.Pesel).HasMaxLength(11);
        builder.HasOne(ic => ic.Client)
            .WithOne(c => c.IndividualClient)
            .HasForeignKey<IndividualClient>(ic => ic.ClientId);

        builder.HasData(new List<IndividualClient>()
        {
            new IndividualClient { ClientId = 1, FirstName = "Jan", LastName = "Kowalski", Pesel = "90010112345" },
            new IndividualClient { ClientId = 2, FirstName = "Anna", LastName = "Nowak", Pesel = "85020223456" },
            new IndividualClient { ClientId = 3, FirstName = "Piotr", LastName = "Wiśniewski", Pesel = "78030334567" },
            new IndividualClient { ClientId = 4, FirstName = "Maria", LastName = "Wójcik", Pesel = "92040445678" },
            new IndividualClient { ClientId = 5, FirstName = "Tomasz", LastName = "Kamiński", Pesel = "88050556789" }
        });
    }
}