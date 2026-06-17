using Microsoft.EntityFrameworkCore;
using System_uznawania_przychodow.Data;
using System_uznawania_przychodow.DTOs.Contracts;
using System_uznawania_przychodow.Entities;
using System_uznawania_przychodow.Exceptions;

namespace System_uznawania_przychodow.Services;

public class ContractService : IContractService
{
    
    private readonly AppDbContext _dbContext;
    public ContractService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<int> CreateContractAsync(CreateContractDto dto)
    {
        var daysDifference = dto.EndDate.DayNumber - dto.StartDate.DayNumber;
        if (daysDifference < 3 || daysDifference > 30)
        {
            throw new InvalidDateRangeException();
        }
        using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var clientExists = await _dbContext.Clients.FirstOrDefaultAsync(c=> c.ClientId==dto.ClientId);
            if (clientExists == null)
            {
                throw new ClientNotFoundException();
            }
            
            var today = DateOnly.FromDateTime(DateTime.Now);
            var hasActiveContract = await _dbContext.Contracts.AnyAsync(c => 
                c.ClientId == dto.ClientId && 
                c.SoftwareId == dto.SoftwareId && 
                ((!c.IsSigned && c.EndDate >= today) || (c.IsSigned && c.StartDate.AddYears(1 + c.SupportYears) >= today)));
            
            if (hasActiveContract)
            {
                throw new ActiveContractAlreadyExistsException();
            }

            var software = await _dbContext.Softwares.FirstOrDefaultAsync(s => s.SoftwareId == dto.SoftwareId);
            if (software == null)
            {
                throw new SoftwareNotFoundException();
            }
            
            var highestDiscount = await _dbContext.Set<SoftwareDiscount>()
                .Where(sd => sd.SoftwareId == dto.SoftwareId && 
                             sd.Discount.StartDate <= today && 
                             sd.Discount.EndDate >= today)
                .Select(sd => sd.Discount.Value)
                .OrderByDescending(v => v)
                .FirstOrDefaultAsync();
            
            var isReturningClient = await _dbContext.Contracts.AnyAsync(c => c.ClientId == dto.ClientId && c.IsSigned);
            decimal additionalDiscount = 0m;
            if (isReturningClient)
            {
                additionalDiscount = 5m;
            }
            
            var basePrice = software.Price + (dto.AdditionalSupportYears * 1000m);
            var priceAfterFirstDiscount = basePrice - (basePrice * (highestDiscount / 100m));
            var finalPrice = priceAfterFirstDiscount - (priceAfterFirstDiscount * (additionalDiscount / 100m));
            var contract = new Contract
            {
                ClientId = dto.ClientId,
                SoftwareId = dto.SoftwareId,
                SoftwareVersion = software.CurrentVersion,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                SupportYears = dto.AdditionalSupportYears,
                Price = finalPrice,
                IsSigned = false
            };

            _dbContext.Contracts.Add(contract);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            return contract.ContractId;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task DeleteContractAsync(int id)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var contract = await _dbContext.Contracts.FirstOrDefaultAsync(c => c.ContractId == id);
            if (contract == null)
            {
                throw new NotFoundException();
            }
            
            if (contract.IsSigned)
            {
                throw new CannotDeleteSignedContractException();
            }

            _dbContext.Contracts.Remove(contract);
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