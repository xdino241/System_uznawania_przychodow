using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System_uznawania_przychodow.Entities;

namespace System_uznawania_przychodow.Configurations;

public class ContractPaymentConfiguration : IEntityTypeConfiguration<ContractPayment>
{
    public void Configure(EntityTypeBuilder<ContractPayment> builder)
    {
        builder.HasKey(cp => cp.ContractPaymentId);
        builder.Property(cp => cp.Amount).HasColumnType("decimal(10,2)");
        builder.HasOne(cp => cp.Contract)
            .WithMany(c => c.Payments)
            .HasForeignKey(cp => cp.ContractId);
    }
}