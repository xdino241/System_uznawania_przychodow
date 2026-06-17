using Microsoft.EntityFrameworkCore;
using System_uznawania_przychodow.DTOs.Contracts;
using System_uznawania_przychodow.Entities;
using System_uznawania_przychodow.Exceptions;
using System_uznawania_przychodow.Services;

namespace Unit_tests;

public class ContractServiceTest
{
    [Fact]
public async Task CreateContractAsync_ValidDataNoDiscounts_CreatesContractWithBasePrice()
{
    var dbContext = TestDbContext.Create();
    var service = new ContractService(dbContext);
    var client = new Client { Email = "klient@example.com", Phone = "500600700", Address = "ul. Testowa 1" };
    dbContext.Clients.Add(client);
    var software = new Software { Name = "TestSoft", Description = "opis", CurrentVersion = "1.0", Category = "Finanse", Price = 5000m };
    dbContext.Softwares.Add(software);
    await dbContext.SaveChangesAsync();
    var dto = new CreateContractDto
    {
        ClientId = client.ClientId,
        SoftwareId = software.SoftwareId,
        StartDate = DateOnly.FromDateTime(DateTime.Now),
        EndDate = DateOnly.FromDateTime(DateTime.Now).AddDays(10),
        AdditionalSupportYears = 0
    };

    var contractId = await service.CreateContractAsync(dto);

    var savedContract = await dbContext.Contracts.FirstOrDefaultAsync(c => c.ContractId == contractId);
    Assert.NotNull(savedContract);
    Assert.Equal(5000m, savedContract.Price);
    Assert.False(savedContract.IsSigned);
}

[Fact]
public async Task CreateContractAsync_ReturningClientWithDiscountAndSupportYears_AppliesCombinedDiscount()
{
    var dbContext = TestDbContext.Create();
    var service = new ContractService(dbContext);
    var client = new Client { Email = "powracajacy@example.com", Phone = "500600700", Address = "ul. Testowa 1" };
    dbContext.Clients.Add(client);
    var software = new Software { Name = "TestSoft", Description = "opis", CurrentVersion = "1.0", Category = "Finanse", Price = 1000m };
    dbContext.Softwares.Add(software);
    await dbContext.SaveChangesAsync();
    var oldSoftware = new Software { Name = "StareSoft", Description = "opis", CurrentVersion = "1.0", Category = "HR", Price = 100m };
    dbContext.Softwares.Add(oldSoftware);
    await dbContext.SaveChangesAsync();
    dbContext.Contracts.Add(new Contract
    {
        ClientId = client.ClientId,
        SoftwareId = oldSoftware.SoftwareId,
        SoftwareVersion = "1.0",
        StartDate = DateOnly.FromDateTime(DateTime.Now).AddYears(-1),
        EndDate = DateOnly.FromDateTime(DateTime.Now).AddYears(-1).AddDays(10),
        SupportYears = 0,
        Price = 100m,
        IsSigned = true
    });


    var discount = new Discount
    {
        Name = "Promo",
        Value = 10m,
        StartDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-1),
        EndDate = DateOnly.FromDateTime(DateTime.Now).AddDays(1)
    };
    discount.SoftwareDiscounts.Add(new SoftwareDiscount { SoftwareId = software.SoftwareId });
    dbContext.Discounts.Add(discount);
    await dbContext.SaveChangesAsync();

    var dto = new CreateContractDto
    {
        ClientId = client.ClientId,
        SoftwareId = software.SoftwareId,
        StartDate = DateOnly.FromDateTime(DateTime.Now),
        EndDate = DateOnly.FromDateTime(DateTime.Now).AddDays(10),
        AdditionalSupportYears = 1 // +1000 PLN
    };

    var contractId = await service.CreateContractAsync(dto);

    var savedContract = await dbContext.Contracts.FirstOrDefaultAsync(c => c.ContractId == contractId);
    Assert.Equal(1710m, savedContract!.Price);
}

[Fact]
public async Task CreateContractAsync_ActiveUnsignedContractExists_ThrowsActiveContractAlreadyExistsException()
{
    var dbContext = TestDbContext.Create();
    var service = new ContractService(dbContext);

    var client = new Client { Email = "klient2@example.com", Phone = "500600700", Address = "ul. Testowa 1" };
    dbContext.Clients.Add(client);
    var software = new Software { Name = "TestSoft", Description = "opis", CurrentVersion = "1.0", Category = "Finanse", Price = 1000m };
    dbContext.Softwares.Add(software);
    await dbContext.SaveChangesAsync();
    
    dbContext.Contracts.Add(new Contract
    {
        ClientId = client.ClientId,
        SoftwareId = software.SoftwareId,
        SoftwareVersion = "1.0",
        StartDate = DateOnly.FromDateTime(DateTime.Now),
        EndDate = DateOnly.FromDateTime(DateTime.Now).AddDays(5),
        SupportYears = 0,
        Price = 1000m,
        IsSigned = false
    });
    await dbContext.SaveChangesAsync();

    var dto = new CreateContractDto
    {
        ClientId = client.ClientId,
        SoftwareId = software.SoftwareId,
        StartDate = DateOnly.FromDateTime(DateTime.Now),
        EndDate = DateOnly.FromDateTime(DateTime.Now).AddDays(10),
        AdditionalSupportYears = 0
    };

    await Assert.ThrowsAsync<ActiveContractAlreadyExistsException>(() => service.CreateContractAsync(dto));
}
}