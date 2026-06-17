using Microsoft.EntityFrameworkCore;
using System_uznawania_przychodow.DTOs.Clients;
using System_uznawania_przychodow.Exceptions;
using System_uznawania_przychodow.Services.ClientServices;

namespace Unit_tests;

public class ClientServiceTest
{
    [Fact]
    public async Task AddIndividualClientAsync_EmailAlreadyExists_ThrowsEmailAlreadyInUseException()
    {
        var dbContext = TestDbContext.Create();
        var service = new ClientService(dbContext);
        var firstDto = new CreateIndividualClientDto
        {
            FirstName = "Jan",
            LastName = "Kowalski",
            Address = "ul. Testowa 1",
            Email = "duplikat@example.com",
            Phone = "500600700",
            Pesel = "90010112345"
        };
        await service.AddIndividualClientAsync(firstDto);
        var secondDto = new CreateIndividualClientDto
        {
            FirstName = "Anna",
            LastName = "Nowak",
            Address = "ul. Inna 5",
            Email = "duplikat@example.com",
            Phone = "501601701",
            Pesel = "85020223456"
        };
        await Assert.ThrowsAsync<EmailAlreadyInUseException>(() => service.AddIndividualClientAsync(secondDto));
    }
 
    [Fact]
    public async Task AddIndividualClientAsync_PhoneWithLetters_ThrowsWrongNumberFormatException()
    {
        var dbContext = TestDbContext.Create();
        var service = new ClientService(dbContext);
 
        var dto = new CreateIndividualClientDto
        {
            FirstName = "Jan",
            LastName = "Kowalski",
            Address = "ul. Testowa 1",
            Email = "jan.test2@example.com",
            Phone = "abc123def", 
            Pesel = "90010112345"
        };
        await Assert.ThrowsAsync<WrongNumberFormatException>(() => service.AddIndividualClientAsync(dto));
    }
    
    [Fact]
    public async Task DeleteClientAsync_IndividualClient_SoftDeletesAndOverwritesData()
    {
        var dbContext = TestDbContext.Create();
        var service = new ClientService(dbContext);
 
        var dto = new CreateIndividualClientDto
        {
            FirstName = "Jan",
            LastName = "Kowalski",
            Address = "ul. Testowa 1",
            Email = "do_usuniecia@example.com",
            Phone = "500600700",
            Pesel = "90010112345"
        };
        
        var clientId = await service.AddIndividualClientAsync(dto);
        await service.DeleteClientAsync(clientId);
        var deletedClient = await dbContext.Clients.Include(c => c.IndividualClient).FirstOrDefaultAsync(c => c.ClientId == clientId);
        Assert.NotNull(deletedClient);
        Assert.True(deletedClient.IsDeleted);
        Assert.Equal("DELETED", deletedClient.IndividualClient!.FirstName);
        Assert.Equal("DELETED", deletedClient.IndividualClient.LastName);
        Assert.Equal("00000000000", deletedClient.IndividualClient.Pesel);
        Assert.Equal("DELETED", deletedClient.Address);
        Assert.Equal("000000000", deletedClient.Phone);
    }
    
    [Fact]
    public async Task DeleteClientAsync_CompanyClient_ThrowsCompanyClientCantBeDeletedException()
    {
        var dbContext = TestDbContext.Create();
        var service = new ClientService(dbContext);
 
        var dto = new CreateCompanyClientDto
        {
            CompanyName = "ABC Sp.",
            Address = "ul. Firmowa 1",
            Email = "firma_delete@example.com",
            Phone = "600700800",
            Krs = "0000123456"
        };
        var clientId = await service.AddCompanyClientAsync(dto);
 
        await Assert.ThrowsAsync<CompanyClientCantBeDeletedException>(() => service.DeleteClientAsync(clientId));
        var companyClient = await dbContext.Clients.Include(c => c.CompanyClient).FirstOrDefaultAsync(c => c.ClientId == clientId);
        Assert.False(companyClient!.IsDeleted);
        Assert.Equal("ABC Sp.", companyClient.CompanyClient!.CompanyName);
    }
    
    [Fact]
    public async Task UpdateIndividualClientAsync_NonExistentClient_ThrowsClientNotFoundException()
    {
        var dbContext = TestDbContext.Create();
        var service = new ClientService(dbContext);

        var updateDto = new UpdateIndividualClientDto
        {
            FirstName = "Janusz",
            LastName = "Nowak",
            Address = "ul. Nowa 2",
            Email = "nieistniejacy@example.com",
            Phone = "501601701"
        };

        await Assert.ThrowsAsync<ClientNotFoundException>(() => service.UpdateIndividualClientAsync(999, updateDto));
    }
    
    [Fact]
    public async Task UpdateCompanyClientAsync_NonExistentClient_ThrowsClientNotFoundException()
    {
        var dbContext = TestDbContext.Create();
        var service = new ClientService(dbContext);
 
        var updateDto = new UpdateCompanyClientDto
        {
            CompanyName = "Nowa Nazwa S.A.",
            Address = "ul. Nowa 2",
            Email = "nieistniejaca_firma@example.com",
            Phone = "601701801"
        };
        
        await Assert.ThrowsAsync<ClientNotFoundException>(() => service.UpdateCompanyClientAsync(999, updateDto));
    }
}