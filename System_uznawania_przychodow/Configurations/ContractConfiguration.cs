using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System_uznawania_przychodow.Entities;

namespace System_uznawania_przychodow.Configurations;

public class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.ToTable("Contracts");
        builder.HasKey(c => c.ContractId);
        builder.Property(c => c.SoftwareVersion).HasMaxLength(20);
        builder.Property(c => c.Price).HasColumnType("decimal(10,2)"); ;
        builder.HasOne(c => c.Client)
            .WithMany(cl => cl.Contracts)
            .HasForeignKey(c => c.ClientId);
        builder.HasOne(c => c.Software)
            .WithMany(s => s.Contracts)
            .HasForeignKey(c => c.SoftwareId);
    }
}