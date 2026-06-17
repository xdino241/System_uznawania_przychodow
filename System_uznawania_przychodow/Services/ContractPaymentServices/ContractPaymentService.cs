using Microsoft.EntityFrameworkCore;
using System_uznawania_przychodow.Data;
using System_uznawania_przychodow.DTOs.Payments;
using System_uznawania_przychodow.Entities;
using System_uznawania_przychodow.Exceptions;

namespace System_uznawania_przychodow.Services.ContractPaymentServices;

public class ContractPaymentService : IContractPaymentService
{
    private readonly AppDbContext _dbContext;

    public ContractPaymentService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<int> AddPaymentAsync(AddPaymentDto dto) 
    {
    using var transaction = await _dbContext.Database.BeginTransactionAsync();
    try
    {
        var contract = await _dbContext.Contracts
            .Include(c => c.Payments) 
            .FirstOrDefaultAsync(c => c.ContractId == dto.ContractId);

        if (contract == null)
        {
            throw new ContractNotFoundException();
        }
        
        if (contract.IsSigned)
        {
            throw new ContractAlreadySignedException();
        }
        
        if (dto.PaymentDate > contract.EndDate)
        {
            if (contract.Payments.Any())
            {
                _dbContext.ContractPayments.RemoveRange(contract.Payments);
                await _dbContext.SaveChangesAsync();
            }
            throw new ContractExpiredException();
        }
        
        var currentTotalPaid = contract.Payments.Sum(p => p.Amount);
        var expectedTotalAfterPayment = currentTotalPaid + dto.Amount;
        if (expectedTotalAfterPayment > contract.Price)
        {
            throw new PaymentAmountExceededException();
        }
        
        var payment = new ContractPayment
        {
            ContractId = dto.ContractId,
            Amount = dto.Amount,
            PaymentDate = dto.PaymentDate
        };
        _dbContext.ContractPayments.Add(payment);
        if (expectedTotalAfterPayment == contract.Price)
        {
            contract.IsSigned = true; 
        }

        await _dbContext.SaveChangesAsync();
        await transaction.CommitAsync();
        return payment.ContractPaymentId;
    }
    catch (Exception)
    {
        await transaction.RollbackAsync();
        throw;
    }
}
}