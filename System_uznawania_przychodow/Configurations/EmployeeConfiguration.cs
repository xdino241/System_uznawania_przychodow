using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System_uznawania_przychodow.Entities;

namespace System_uznawania_przychodow.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");
        builder.HasKey(c => c.EmployeeId);
        builder.Property(c => c.Login).HasMaxLength(30);
        builder.Property(c=> c.Password).HasMaxLength(100);
        builder.Property(c => c.Role).HasMaxLength(30);
        
        builder.HasData(new List<Employee>(){ //Password: admin123
            new Employee { EmployeeId = 1, Login = "admin", Password = "$2b$12$BWVZDJ9rQlAAhIA4QiL2gO2LjF9k5m60UbjRH20aUdJ3sPW1Xnke2", Role = "Admin" },
            //Password: pracownik123
            new Employee { EmployeeId = 2, Login = "pracownik", Password = "$2b$12$0cyiYHxNDyrenE3oW94bieX9SeaHdgeftyayW7p63LD5FEdfhGsCO", Role = "User" }
            }
        );
    }
}