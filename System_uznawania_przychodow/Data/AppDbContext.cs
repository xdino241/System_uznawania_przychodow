using Microsoft.EntityFrameworkCore;
using System_uznawania_przychodow.Entities;

namespace System_uznawania_przychodow.Data;

public class AppDbContext : DbContext
{
    protected AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Client> Clients { get; set; }
    public DbSet<CompanyClient>  CompanyClients { get; set; }
    public DbSet<IndividualClient> IndividualClients { get; set; }
    public DbSet<Software> Softwares { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<SoftwareDiscount> SoftwareDiscounts { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<ContractPayment> ContractPayments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}