using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System_uznawania_przychodow.Entities;

namespace System_uznawania_przychodow.Configurations;

public class SoftwareConfiguration : IEntityTypeConfiguration<Software>
{
    public void Configure(EntityTypeBuilder<Software> builder)
    {
        builder.ToTable("Softwares");
        builder.HasKey(s => s.SoftwareId);
        builder.Property(s => s.Name).HasMaxLength(50);
        builder.Property(s => s.Description).HasMaxLength(500);
        builder.Property(s => s.CurrentVersion).HasMaxLength(20);
        builder.Property(s => s.Category).HasMaxLength(100);
        builder.Property(s => s.Price).HasColumnType("decimal(10,2)");
        
        builder.HasData(new List<Software>(){
            new Software { SoftwareId = 1, Name = "FinanSoft", Description = "System do zarządzania finansami", CurrentVersion = "3.2.1", Category = "Finanse", Price = 5000.00m },
            new Software { SoftwareId = 2, Name = "EduPlatform", Description = "Platforma e-learningowa", CurrentVersion = "2.0.5", Category = "Edukacja", Price = 3500.00m },
            new Software { SoftwareId = 3, Name = "HRManager", Description = "System do zarządzania HR", CurrentVersion = "4.1.0", Category = "HR", Price = 7000.00m },
            new Software { SoftwareId = 4, Name = "WarehousePro", Description = "Magazyn i logistyka", CurrentVersion = "1.8.3", Category = "Logistyka", Price = 8500.00m },
            new Software { SoftwareId = 5, Name = "CRM Master", Description = "System CRM", CurrentVersion = "5.0.0", Category = "Sprzedaż", Price = 4200.00m },
            new Software { SoftwareId = 6, Name = "AccountKeeper", Description = "Księgowość", CurrentVersion = "2.3.7", Category = "Finanse", Price = 2000.00m }
        });
    }
}