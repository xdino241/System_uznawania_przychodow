using Microsoft.EntityFrameworkCore;
using System_uznawania_przychodow.Data;
using System_uznawania_przychodow.DTOs.Clients;
using System_uznawania_przychodow.Entities;
using System_uznawania_przychodow.Exceptions;

namespace System_uznawania_przychodow.Services.ClientServices;

public class ClientService : IClientService
{
    private readonly AppDbContext _dbContext;
    
    public ClientService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> AddIndividualClientAsync(CreateIndividualClientDto dto)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var emailExists = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Email == dto.Email);
            if (emailExists != null)
            {
                throw new EmailAlreadyInUseException();
            }
            
            if (!dto.Phone.All(char.IsDigit))
            {
                throw new WrongNumberFormatException("Phone number should have only numbers");
            }
            
            var client = new Client
            {
                Address = dto.Address,
                Email = dto.Email,
                Phone = dto.Phone
            };
            _dbContext.Clients.Add(client);
            await _dbContext.SaveChangesAsync(); 
            
            var individualClient = new IndividualClient
            {
                ClientId = client.ClientId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Pesel = dto.Pesel
            };
            _dbContext.IndividualClients.Add(individualClient);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            return client.ClientId;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<int> AddCompanyClientAsync(CreateCompanyClientDto dto)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var emailExists = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Email == dto.Email);
            if (emailExists != null)
            {
                throw new EmailAlreadyInUseException();
            }
            
            if (!dto.Phone.All(char.IsDigit))
            {
                throw new WrongNumberFormatException();
            }

            var client = new Client
            {
                Address = dto.Address,
                Email = dto.Email,
                Phone = dto.Phone
            };
            _dbContext.Clients.Add(client);
            await _dbContext.SaveChangesAsync();

            var companyClient = new CompanyClient()
            {
                ClientId = client.ClientId,
                CompanyName = dto.CompanyName,
                Krs = dto.Krs
            };
            _dbContext.CompanyClients.Add(companyClient);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            return client.ClientId;
        }
        catch(Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task DeleteClientAsync(int id)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var client = await _dbContext.Clients
                .Include(c => c.IndividualClient)
                .FirstOrDefaultAsync(c => c.ClientId == id);
            
            if (client == null)
            {
                throw new ClientNotFoundException();
            }
            if (client.IndividualClient == null)
            {
                throw new CompanyClientCantBeDeletedException();
            }
            client.IndividualClient.FirstName = "DELETED";
            client.IndividualClient.LastName = "DELETED";
            client.IndividualClient.Pesel = "00000000000";
            client.IsDeleted = true;
            client.Email = $"deleted_{id}@deleted.com";
            client.Phone = "000000000";
            client.Address = "DELETED";
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }

    }

    public async Task UpdateIndividualClientAsync(int id, UpdateIndividualClientDto dto)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var emailExists = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Email == dto.Email);
            if (emailExists != null)
            {
                throw new EmailAlreadyInUseException();
            }
            
            var client = await _dbContext.Clients
                .Include(c => c.IndividualClient)
                .FirstOrDefaultAsync(c => c.ClientId == id);
            
            if (client == null || client.IndividualClient == null)
            {
                throw new ClientNotFoundException();
            }
            client.IndividualClient.FirstName = dto.FirstName;
            client.IndividualClient.LastName = dto.LastName;
            client.Address = dto.Address;
            client.Email = dto.Email;
            client.Phone = dto.Phone;
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task UpdateCompanyClientAsync(int id, UpdateCompanyClientDto dto)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var emailExists = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Email == dto.Email);
            if (emailExists != null)
            {
                throw new EmailAlreadyInUseException();
            }
            
            var client = await _dbContext.Clients
                .Include(c => c.CompanyClient)
                .FirstOrDefaultAsync(c => c.ClientId == id);
            
            if (client == null || client.CompanyClient == null)
            {
                throw new ClientNotFoundException();
            }
            client.CompanyClient.CompanyName = dto.CompanyName;
            client.Address = dto.Address;
            client.Email = dto.Email;
            client.Phone = dto.Phone;
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}